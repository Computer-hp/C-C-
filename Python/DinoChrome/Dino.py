from tkinter import *
from PIL import Image, ImageTk
import random

global started
started = FALSE

class Dino:
    def __init__(self, canvas): 
        self.width = 50
        self.height = 55
        x = 20
        y = 300 - self.height - 10
        self.canvas = canvas
        
        from PIL import Image, ImageTk
        # Load and resize the image
        image = Image.open("Dino\\DinoJump.png")  # Replace "dino.png" with the path to your Dino image
        image = image.resize((self.width, self.height), Image.LANCZOS)
        self.dino_image = ImageTk.PhotoImage(image)
        
        # Create a canvas image item with the loaded image
        self.dino_id = canvas.create_image(x, y, anchor=NW, image=self.dino_image)
        self.jumpSpeed = DINOJUMP

    def down(self):
        dino_coords = self.canvas.coords(self.dino_id)
        if dino_coords[1] >= 290 - 55:
            x = 20
            y = 300 - self.height - 10
            self.canvas.coords(self.dino_id, x, y)
            return
        self.canvas.move(self.dino_id, 0, +self.jumpSpeed)
        window.after(10, self.down)

    def jump(self):
        dino_coords = self.canvas.coords(self.dino_id)
        #print(cactus_coords)
        if dino_coords[1] <= 90:  # Check if the cactus is completely outside the canvas
            self.down()
            return
        # elif dino_coords[1] <= 140:
        #     self.canvas.move(self.dino_id, 0, -self.jumpSpeed)
        self.canvas.move(self.dino_id, 0, -self.jumpSpeed)
        window.after(10, self.jump)

class Cactus:
    def __init__(self, canvas):
        # Only the height because the width is variable
        self.setCoord()
        self.canvas = canvas
        self.speed = CACTUSSPEED  # Adjust the speed of the animation
        return

    def setCoord(self):
        self.height = 60
        width = [25, 40, 45, 30, 60, 65]
        random_width = random.choice(width)
        self.width = random_width
        x = 500 - self.width
        y = 300 - self.height - 10  # cactus.height = 60
        # To set the image --> self.cactus_image
        self.setImage()

        if (not started): 
            self.cactus_id = canvas.create_image(x, y, anchor = NW, image = self.cactus_image)
        else:
            self.canvas.coords(self.cactus_id, x, y)
        return

    def setImage(self):
        from PIL import Image
        image = None

        if (self.width == 25):
            image = Image.open("Cactus\\SmallCactus1.png")
            image = image.resize((self.width, self.height), Image.LANCZOS)

        elif (self.width == 40):
            image = Image.open("Cactus\\SmallCactus2.png")
            image = image.resize((self.width, self.height), Image.LANCZOS)

        elif (self.width == 45):
            image = Image.open("Cactus\\SmallCactus3.png")
            image = image.resize((self.width, self.height), Image.LANCZOS)

        elif (self.width == 30):
            image = Image.open("Cactus\\LargeCactus1.png")
            image = image.resize((self.width, self.height), Image.LANCZOS)
            
        elif (self.width == 60):
            image = Image.open("Cactus\\LargeCactus2.png")
            image = image.resize((self.width, self.height), Image.LANCZOS)
            
        elif (self.width == 65):
            image = Image.open("Cactus\\LargeCactus3.png")
            image = image.resize((self.width, self.height), Image.LANCZOS)
        self.cactus_image = ImageTk.PhotoImage(image)
        return

    def move(self):
        #                                     x       y
        self.canvas.move(self.cactus_id, -self.speed, 0)
        cactus_coords = self.canvas.coords(self.cactus_id)
        #print(cactus_coords)
        if cactus_coords[0] < -60:  # Check if the cactus is completely outside the canvas
            self.reset_position()

    def reset_position(self):
        #self.setImage()
        self.setCoord()


def animate():
    CACTUS.move()
    # widget.after(delay_in_milliseconds, function_name)
    window.after(10, animate)

DINOJUMP = 7
CACTUSSPEED = 5
# window
window = Tk()
window.title("DinoGame")
window.configure(width = 500, height = 300)
window.configure(bg = 'lightgray')

# To make objects appear in the window
canvas = Canvas(window, width = 500, height = 300, bg = 'lightgray')
canvas.pack()

# Objects
DINO = Dino(canvas)
CACTUS = Cactus(canvas)

def key_press(event):
    if event.keysym == "Return" or event.keysym == "space":
        DINO.jump()

# To bind the pressed key with the variable key_press
window.bind("<KeyPress>", key_press)

animate()
window.mainloop()