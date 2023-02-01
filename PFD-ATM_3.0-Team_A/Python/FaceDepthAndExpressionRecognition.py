import cv2
import cvzone
from cvzone.FaceMeshModule import FaceMeshDetector
from deepface import DeepFace
import sys
import os
import time
from VideoWriter import *

cap = cv2.VideoCapture(0)
detector = FaceMeshDetector(maxFaces=2)
# detected_emotions = ["Angry", "Disgust", "Fear", "Happy", "Sad", "Surprise", "Neutral"]
start = time.time()

videoWriterScript = 'VideoWriter.py'

while True:
    success, img = cap.read()
    img, faces = detector.findFaceMesh(img, draw=False)
    faces_depth = [0, 0]

    if faces:
        for face in faces:
            pointLeft = face[145]
            pointRight = face[374]

            w, _ = detector.findDistance(pointLeft, pointRight)
            W = 6.3  # Average of eye width of men n women

            # Finding the Focal Length
            # d = 50
            # f = (w*d)/W
            # print(f)

            # Finding distance
            f = 571
            d = (W * f) / w
            # print(int(d))

            # Getting list of face depth
            faces_depth[faces.index(face)] = d

            # Facial Expression Recognition
            face_emotions = DeepFace.analyze(img, actions=['emotion'], prog_bar=False, enforce_detection=False)
            fear_lvl = face_emotions["emotion"]["fear"]

            if fear_lvl < 80:
                colour = (0, 220, 255)
            else:
                colour = (0, 140, 255)

            msg = "Fear: {:.1f}".format(fear_lvl)
            cv2.putText(img, msg, (pointRight[0] + 40, pointRight[1] - 40),
                        cv2.FONT_HERSHEY_SIMPLEX, 0.5, colour, 1, cv2.LINE_AA, )

        if fear_lvl < 80:
            start = time.time()
            print("ferfalse")
            sys.stdout.flush()
        else:
            if time.time() - start > 3:

                WriteVid()
                vid_link = StoreVid()
                with open("Static/vid.txt","w") as file:
                    file.write(vid_link)

                print("fertrue")
                sys.stdout.flush()

                cap = cv2.VideoCapture(0)
                detector = FaceMeshDetector(maxFaces=2)
                start = time.time()
                    
                continue

    if len(faces) > 1:
        cvzone.putTextRect(img, f'Depth Difference: {int(abs(faces_depth[0] - faces_depth[1]))}cm', (400, 50), scale=1, thickness=2)
        print("fd"+str(int(abs(faces_depth[0] - faces_depth[1]))))
        sys.stdout.flush()

        if abs(faces_depth[0] - faces_depth[1]) < 75:
            cv2.rectangle(img, (280, 230), (400, 280), (0, 255, 0), cv2.FILLED)
            cv2.putText(img, 'Warning', (285, 265), cv2.FONT_HERSHEY_TRIPLEX, 0.8, (0, 0, 0), 2)

        cv2.imshow("Image", img)
        cv2.waitKey(10)  # wait one millisecond between capturing
        continue

    print("fd999");
    sys.stdout.flush();

    cv2.imshow("Image", img)
    cv2.waitKey(10)  # wait one millisecond between capturing
    if cv2.getWindowProperty("Image", cv2.WND_PROP_VISIBLE) < 1:
        print("ferfalse")
        sys.stdout.flush()
        break
cap.release()
cv2.destroyAllWindows();


