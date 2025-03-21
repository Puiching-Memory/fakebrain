
import socket
import time
import pyautogui # pip install pyautogui==0.9.54
import random

# 服务器配置
SERVER_IP = "127.0.0.1"  # 服务器IP地址
SERVER_PORT = 9529       # 服务器端口
SEND_INTERVAL = 0.1      # 发送间隔（秒）

# max_screen = (1702, 1062)
# min_screen = (5,5)

def get_mouse_position():
    """获取当前鼠标坐标"""
    x, y = pyautogui.position()
    return f"{x},{y}"

def send_mouse_position():
    """建立TCP连接并发送坐标"""
    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    
    try:
        # 连接到服务器
        client_socket.connect((SERVER_IP, SERVER_PORT))
        print(f"Connected to server at {SERVER_IP}:{SERVER_PORT}")
        
        while True:
            # 获取坐标
            pos = get_mouse_position()
            # 发送数据（添加换行符作为消息分隔符）
            message = f"1,{pos}".encode("utf-8")
            client_socket.sendall(message)
            print(f"Sent: {message.decode().strip()}")

            # 发送眨眼检测
            message = f"2,{random.randint(0,100)},{random.randint(0,1)}".encode("utf-8")
            client_socket.sendall(message)
            print(f"Sent: {message.decode().strip()}")
            
            # 等待间隔
            time.sleep(SEND_INTERVAL)
            
    except Exception as e:
        print(f"Error: {e}")
    finally:
        client_socket.close()
        print("Connection closed")

if __name__ == "__main__":
    send_mouse_position()