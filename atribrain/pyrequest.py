import json
import requests
import base64
from PIL import Image
from io import BytesIO

response = requests.get("http://yun.ihnnk.com:50080/user/captcha")

code = json.loads(response.text) # {data:{id,image},error,message}
print(code["data"]["id"])
image = base64.b64decode(code["data"]["image"])
image = BytesIO(image)
image = Image.open(image)

image.show()