import sys
import os

sys.path.append(os.path.abspath("./"))

import torch
import torch.nn as nn
import torch.nn.functional as F


class model(nn.Module):
    def __init__(self):
        super().__init__()
        # self.lstm = nn.LSTM(1, 90, 2,dropout=0.5)
        self.layers = nn.Sequential(
            nn.Conv1d(3, 32, kernel_size=3, padding=1),
            nn.SiLU(),
            nn.Conv1d(32, 64, kernel_size=3, padding=1),
            nn.SiLU(),
            nn.Conv1d(64, 128, kernel_size=3, padding=1),
            nn.SiLU(),

            # head
            nn.Conv1d(128, 1, kernel_size=1),
            nn.LazyLinear(2)
        )

    def forward(self, x: torch.Tensor) -> torch.Tensor:
        # x (149,1,1)
        x = self.layers(x)

        return x


if __name__ == "__main__":
    test_model = model()
    test_model.to("xpu")

    for i in range(10000):

        x = torch.rand(3, 2000, device="xpu")
        y = test_model(x)

        print(x.device)
        print(y.device)
    # print(x)
    # print(y)