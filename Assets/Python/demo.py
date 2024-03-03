import time
import random

class BaseStation:
    def __init__(self, id, coverage_area, position):
        self.id = id
        self.coverage_area = coverage_area
        self.position = position
        self.connected_devices = set()

    def add_device(self, device_id):
        self.connected_devices.add(device_id)

    def remove_device(self, device_id):
        if device_id in self.connected_devices:
            self.connected_devices.remove(device_id)

    def calculate_signal_strength(self, device_position):
        distance = abs(self.position - device_position)
        # Simple model for signal strength based on distance
        return 100 / (distance + 1)  # Signal strength inversely proportional to distance

    def send_data(self, device_id, data):
        print(f"Cell {self.id} Data received from device {device_id}: {data}")

class MobileDevice:
    def __init__(self, id, current_cell, position, data):
        self.id = id
        self.current_cell = current_cell
        self.position = position
        self.data = data

    def move(self):
        # Simulate movement of the device
        self.position += 20  # Randomly move between -5 and 5 units
        print(f"Current position of device: {self.position}")

    def initiate_handover(self, new_cell):
        print(f"Initiating handover for device {self.id} from cell {self.current_cell.id} to cell {new_cell.id}")
        remaining_data = self.data
        self.current_cell = new_cell
        #new_cell.send_data(self.id, remaining_data)
        self.data = ""

# Create base stations
cell_A = BaseStation("A", coverage_area=(0, 100), position=50)
cell_B = BaseStation("B", coverage_area=(101, 200), position=150)

# Create mobile device
device = MobileDevice("1", current_cell=cell_A, position=60, data="Hello, this is a test message.")

# Connect the device to cell A
cell_A.add_device(device.id)

ddata = "Hello, this is a test message.".split(" ")
enudata = enumerate(ddata)
listenu = []
for each in enudata:
    listenu.append(each[1])
print(listenu)
# Simulate movement and handover based on signal strength
while len(listenu) != 0:
    # Move the mobile device
    device.move()
    device.current_cell.send_data(device.id, listenu[0])
    listenu.pop(0)
    # Calculate signal strengths from both cells
    signal_strength_A = cell_A.calculate_signal_strength(device.position)
    signal_strength_B = cell_B.calculate_signal_strength(device.position)
    
    # Check if a handover should occur
    if signal_strength_B > signal_strength_A and device.current_cell != cell_B:
        device.initiate_handover(cell_B)
        cell_A.remove_device(device.id)
        cell_B.add_device(device.id)
    
    # Sleep for a short duration to simulate time passing
    time.sleep(0.1)

