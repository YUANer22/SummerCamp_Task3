import os
from PIL import Image

# create new directory for icons
if not os.path.exists("人物头像"):
    os.mkdir("人物头像")

# loop through all subdirectories in "人物动画"
for root, dirs, files in os.walk("人物动画"):
    for dir in dirs:
        # check if directory name contains "待机"
        if "待机" in dir:
            # get path to "idle_00.png" in current directory
            icon_path = os.path.join(root, dir, "idle_00.png")
            # open image and resize to square dimensions
            with Image.open(icon_path) as img:
                size = min(img.size)
                img = img.crop((0, 0, size, size))
                img = img.resize((256, 256))
                # save resized image to "人物头像" directory
                icon_name = f"{dir}.png"
                icon_path = os.path.join("人物头像", icon_name)
                img.save(icon_path)
