import pygame
import random

from utils import scale_image

pygame.init()

DINO_IMG = scale_image(pygame.image.load("assets\\dino_1.png"), 0.6)

global isjumping
isjumping = False

SMALL_CACTUS1 = scale_image(pygame.image.load("Cactus\\SmallCactus1.png"), 0.7)
SMALL_CACTUS2 = scale_image(pygame.image.load("Cactus\\SmallCactus2.png"), 0.7)
SMALL_CACTUS3 = scale_image(pygame.image.load("Cactus\\SmallCactus3.png"), 0.7)

LARGE_CACTUS1 = scale_image(pygame.image.load("Cactus\\LargeCactus1.png"), 0.7)
LARGE_CACTUS2 = scale_image(pygame.image.load("Cactus\\LargeCactus2.png"), 0.7)
LARGE_CACTUS3 = scale_image(pygame.image.load("Cactus\\LargeCactus3.png"), 0.7)

class Dino:
    def __init__(self):
        self.x = 20
        self.y = 300 - DINO_IMG.get_height() - 10
        self.img = DINO_IMG
        self.vel = 10
        self.mass = 1
        

    def jump(self):
        clock.tick(FPS)
        # Indicates pygame is running

        # drawing object on screen which is rectangle here
        # pygame.draw.rect(win, (255, 0, 0), (x, y, width, height))
        
        # iterate over the list of Event objects
        # that was returned by pygame.event.get() method.

        # stores keys pressed
        # keys = pygame.key.get_pressed()
            
        # if isjump == False:

        #     # if space bar is pressed
        #     if keys[pygame.K_SPACE]:   
        #         # make isjump equal to True
        #         isjump = True
                
        if isjumping:
            # calculate force (F). F = 1 / 2 * mass * velocity ^ 2.
            F = (1 / 2) * self.mass * (self.vel ** 2)
            
            # change in the y co-ordinate
            self.y -= F
            
            # decreasing velocity while going up and become negative while coming down
            self.vel = self.vel - 1
            
            # object reached its maximum height
            if self.vel < 0:
                
                # negative sign is added to counter negative velocity
                self.mass = -1

            # objected reaches its original state
            if self.vel == -11:
                # making isjump equal to false
                isjumping = False
        
                # setting original values to v and m
                self.vel = 10
                self.mass = 1
        
        # creates time delay of 10ms
            pygame.time.delay(20)
            # it refreshes the window
            # pygame.display.update()
            self.jump()

        else:
            return

    def draw(self, win):
        win.blit(self.img, (self.x, self.y))
        pygame.display.update()
        
# lista cactus
# sceglie uno casualmente
# self.image = casual_image
# funzione che fa andare il cactus

class Cactus:
    def __init__(self):
        self.vel = 6
        self.setCoord()

    def setCoord(self):
        cactus_img = [SMALL_CACTUS1, SMALL_CACTUS2, SMALL_CACTUS3, LARGE_CACTUS1, LARGE_CACTUS2, LARGE_CACTUS3]
        rnd_img = random.choice(cactus_img)
        self.img = rnd_img
        self.x = 500 - self.img.get_width() - 10
        self.y = 300 - self.img.get_height() - 10
    
    def slide(self):
        clock.tick(FPS)
        if self.x < -60:            
            self.x -= self.vel
        
        # creates time delay of 10ms
            pygame.time.delay(20)
            # it refreshes the window
            # pygame.display.update()
            self.slide()

        else:
            self.setCoord()
    
    def draw(self, win):
        win.blit(self.img, (self.x, self.y))
        pygame.display.update()

def draw(win, DINO, CACTUS):
    DINO.draw(win)
    CACTUS.draw(win)

# window
WIDTH = 500
HEIGHT = 300
WIN = pygame.display.set_mode((WIDTH, HEIGHT))
# backgroud window color = lightgray color
WIN.fill((211, 211, 211))
pygame.display.set_caption("Dino Adventure!")

FPS = 60

clock = pygame.time.Clock()

for event in pygame.event.get():
            
            # if event object type is QUIT
            # then quitting the pygame
            # and program both.
            if event.type == pygame.QUIT:
                # it will make exit the while loop
                pygame.quit()

# def draw(win, images, player_car):
#     for img, pos in images:
#         win.blit(img, pos)

    # DINO.draw(win)
    # CACTUS.draw(win)
    # pygame.display.update()

# Objects
DINO = Dino()
CACTUS = Cactus()
run = True

while run:
    draw(WIN, DINO, CACTUS)
    # draw(WIN, images, CACTUS)
    # To bind the pressed key with the variable key_press
    key_press = pygame.key.get_pressed()

    if  key_press[pygame.K_RETURN] or key_press[pygame.K_SPACE]:
        isjumping = True
        DINO.jump()
    # elif (key_press[pygame.K_r]):
    #     run = True
    # else:
    #     run = False

pygame.quit()