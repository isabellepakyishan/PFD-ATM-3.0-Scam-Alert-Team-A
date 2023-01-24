import cv2
import os
import sys
import time
import boto3
from datetime import datetime

def WriteVid():
    cap = cv2.VideoCapture(0)

    # Define the codec and create VideoWriter object
    width= int(cap.get(cv2.CAP_PROP_FRAME_WIDTH))
    height= int(cap.get(cv2.CAP_PROP_FRAME_HEIGHT))
    fourcc = cv2.VideoWriter_fourcc("D","I","V","X")
    video_file_count = 1

    start = time.time()
    ##video_file = os.path.join("output", str(video_file_count) + ".avi")
    out = cv2.VideoWriter("Static/output.avi", fourcc, 20.0, (width, height))

    while True:
        success, img = cap.read()
        if success:
            cv2.imshow("vid", img)

            out.write(img)
            cv2.waitKey(10)  # wait one millisecond between capturing

            if cv2.getWindowProperty("vid", cv2.WND_PROP_VISIBLE) < 1 or time.time() - start > 15:
                break
    ##        if time.time() - start > 15:
    ##            break
        else:
            print("fail")
            break
    out.release()
    cap.release()
    cv2.destroyAllWindows()

def StoreVid():
    try:
        s3 = boto3.client('s3')

        bucket_name = 'mypfdalertbucket'
        vid_path = 'Static/output.avi'
        inner_vid_path = datetime.now().strftime("%Y-%m-%d-%H-%M-%S") + vid_path.split('/')[-1]
        s3.upload_file(vid_path, bucket_name, inner_vid_path)

        vid_link = s3.generate_presigned_url('get_object', Params={'Bucket': bucket_name, 'Key': inner_vid_path})
    except Exception as e:
        return str(e)
    else:
        return vid_link
