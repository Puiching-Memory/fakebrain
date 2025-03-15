import socket
import json

# 定义服务器地址和端口号
HOST = '127.0.0.1'  # 本机回环地址
PORT = 9529         # 监听的端口号

# 创建一个TCP/IP套接字
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# 绑定套接字到地址和端口
server_socket.bind((HOST, PORT))

# 开始监听请求
server_socket.listen(1)
print(f"Listening on {HOST}:{PORT}")

while True:
    # 等待连接
    client_socket, addr = server_socket.accept()
    print(f"Connected by {addr}")

    try:
        while True:
            # 接收数据
            data = client_socket.recv(1024)  # 缓冲区大小为1024字节
            if not data:
                # 如果没有数据，则表示客户端关闭了连接
                break
            print(f"Received: {data.decode()}")

    finally:
        # 关闭连接
        client_socket.close()
        print("Connection closed")