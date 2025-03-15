import os
import numpy as np
import torch
import torch.utils.data as data
import torch.nn.functional as F
import time
import sys

sys.path.append(os.path.abspath("./"))

class EDFDataset(data.Dataset):
    def __init__(self, root_dir):
        # TODO: 载入数据
        print(f"Loading data from {root_dir}")
        for path in os.listdir(root_dir):
            print(path)
        self.idx_list = []

    def __len__(self):
        return len(self.idx_list)

    def __getitem__(self, index):
        index_string = self.idx_list[index]
        

        # return inputs, labels
        return index


if __name__ == "__main__":
    from torch.utils.data import DataLoader

    dataset = EDFDataset(
        r"C:\workspace\github\fakebrain\atribrain\data"
    )
    dataloader = DataLoader(dataset=dataset, batch_size=1, shuffle=True,num_workers=0,pin_memory=True)

    for batch_idx, (inputs, targets, info) in enumerate(dataloader):
        print(f"inputs: {inputs.shape}")
        for k, v in zip(targets.keys(), targets.values()):
            print(f"output-{k}:{targets[k].shape}")
        print(f"info: {info}")

        break