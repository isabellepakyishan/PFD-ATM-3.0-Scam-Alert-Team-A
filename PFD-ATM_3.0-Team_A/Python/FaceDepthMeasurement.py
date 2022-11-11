import cv2
import cvzone
from cvzone.FaceMeshModule import FaceMeshDetector
import sys

cap = cv2.VideoCapture(0)
detector = FaceMeshDetector(maxFaces=2)

while True:
    success, img = cap.read()
    img, faces = detector.findFaceMesh(img, draw=False)
    faces_depth = [0, 0]

    if faces:
        for face in faces:
            pointLeft = face[145]
            pointRight = face[374]
            # Drawing
            # cv2.line(img, pointLeft, pointRight, (0, 200, 0), 3)
            # cv2.circle(img, pointLeft, 5, (255, 0, 255), cv2.FILLED)
            # cv2.circle(img, pointRight, 5, (255, 0, 255), cv2.FILLED)
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

            # Show depth of face from camera
            # cvzone.putTextRect(img, f'Depth: {int(d)}cm',
            #                    (face[10][0] - 100, face[10][1] - 50),
            #                    scale=2)
    if len(faces) > 1:
        cvzone.putTextRect(img, f'Depth Difference: {int(abs(faces_depth[0] - faces_depth[1]))}cm', (400, 50), scale=1, thickness=2)
        print(str(int(abs(faces_depth[0] - faces_depth[1]))))
        sys.stdout.flush()

#        @app.route('/depth')
#        def depth_diff_post():
#            depth = int(abs(faces_depth[0] - faces_depth[1]))
#            return jsonify(depth_diff = depth)

#        try:
#            async def depthOutput():
#                async with async_open('depth.txt', 'w') as f:
#                   await f.write(str(int(abs(faces_depth[0] - faces_depth[1]))))
#                    f.close()
#        except:
#            print("File is being access currently.")

        if abs(faces_depth[0] - faces_depth[1]) < 50:
            cv2.rectangle(img, (280, 230), (400, 280), (0, 255, 0), cv2.FILLED)
            cv2.putText(img, 'Warning', (285, 265), cv2.FONT_HERSHEY_TRIPLEX, 0.8, (0, 0, 0), 2)

        cv2.imshow("Image", img)
        cv2.waitKey(1)  # wait one millisecond between capturing
        continue

    print("999");
    sys.stdout.flush();
    cv2.imshow("Image", img)
    cv2.waitKey(1)  # wait one millisecond between capturing
    if cv2.getWindowProperty("Image", cv2.WND_PROP_VISIBLE) < 1:
        break
cv2.destroyAllWindows();
