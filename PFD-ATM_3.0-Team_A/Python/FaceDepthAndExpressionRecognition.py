import cv2
import cvzone
from cvzone.FaceMeshModule import FaceMeshDetector
from deepface import DeepFace
import sys

cap = cv2.VideoCapture(0)
detector = FaceMeshDetector(maxFaces=2)
# detected_emotions = ["Angry", "Disgust", "Fear", "Happy", "Sad", "Surprise", "Neutral"]

while True:
    success, img = cap.read()
    img, faces = detector.findFaceMesh(img, draw=False)
    faces_depth = [0, 0]

    if faces:
        for face in faces:
            pointLeft = face[145]
            pointRight = face[374]
            de_index = 0

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
            for emotion in face_emotions["emotion"]:
                if emotion == "fear":
                    print("fer{:.1f}".format(face_emotions["emotion"][emotion]))
                    sys.stdout.flush()
                else:
                    continue

                if face_emotions["emotion"][emotion] > 80:
                    colour = (0, 140, 255)
                else:
                    colour = (0, 220, 255)

                # msg = "{}: {:.1f}".format(detected_emotions[de_index], face_emotions["emotion"][emotion])
                msg = "Fear: {:.1f}".format(face_emotions["emotion"][emotion])
                cv2.putText(img, msg, (pointRight[0] + 40, pointRight[1] - 40 + de_index * 15),
                            cv2.FONT_HERSHEY_SIMPLEX, 0.5, colour, 1, cv2.LINE_AA, )
                de_index += 1
                break

    if len(faces) > 1:
        cvzone.putTextRect(img, f'Depth Difference: {int(abs(faces_depth[0] - faces_depth[1]))}cm', (400, 50), scale=1, thickness=2)
        print("fd"+str(int(abs(faces_depth[0] - faces_depth[1]))))
        sys.stdout.flush()

        if abs(faces_depth[0] - faces_depth[1]) < 75:
            cv2.rectangle(img, (280, 230), (400, 280), (0, 255, 0), cv2.FILLED)
            cv2.putText(img, 'Warning', (285, 265), cv2.FONT_HERSHEY_TRIPLEX, 0.8, (0, 0, 0), 2)

        cv2.imshow("Image", img)
        cv2.waitKey(1)  # wait one millisecond between capturing
        continue

    print("fd999");
    sys.stdout.flush();

    cv2.imshow("Image", img)
    cv2.waitKey(1)  # wait one millisecond between capturing
    if cv2.getWindowProperty("Image", cv2.WND_PROP_VISIBLE) < 1:
        print("fer0.0")
        sys.stdout.flush()
        break
cv2.destroyAllWindows();


