# tire_volume.py
import math
from datetime import date

# Get input from the user
width = int(input("Enter the width of the tire in mm (e.g. 205): "))
aspect_ratio = int(input("Enter the aspect ratio of the tire (e.g. 60): "))
diameter = int(input("Enter the diameter of the wheel in inches (e.g. 15): "))

# Calculate the volume using the given formula
volume = (math.pi * width**2 * aspect_ratio * (width * aspect_ratio + 2540 * diameter)) / 10000000000

# Display the result
print(f"\nThe approximate volume is {volume:.2f} liters.")

# Get the current date
today = date.today()

# Open the file in append mode and write the data
with open("volumes.txt", "a") as file:
    file.write(f"{today}, {width}, {aspect_ratio}, {diameter}, {volume:.2f}\n")