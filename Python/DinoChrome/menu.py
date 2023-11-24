import pygame
import random

from utils import scale_image

pygame.init()

DINO_IMG = scale_image(pygame.image.load("assets\\dino_1.png"), 0.6)

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
        self.mass = 0.85
        self.isjumping = False

    def jump(self):
        if self.isjumping:
            # calculate force (F). F = 1 / 2 * mass * velocity ^ 2.
            F = (1 / 2) * self.mass * (self.vel ** 2)

            # change in the y co-ordinate
            self.y -= F
            print(self.y)
            # decreasing velocity while going up and become negative while coming down
            self.vel = self.vel - 1
            
            # object reached its maximum height
            if self.vel < 0:
                # negative sign is added to counter negative velocity
                self.mass = -0.65

            # object reaches its original state
            if self.vel == -11:
                # making isjump equal to false
                self.isjumping = False
                # setting original values to v and m
                self.vel = 10
                self.mass = 0.85

        else:
            self.y = 300 - DINO_IMG.get_height() - 10  # Reset the y position

    def draw(self, win):
        win.blit(self.img, (self.x, self.y))

class Cactus:
    def __init__(self):
        self.vel = 7.5
        self.setCoord()

    def setCoord(self):
        cactus_img = [SMALL_CACTUS1, SMALL_CACTUS2, SMALL_CACTUS3, LARGE_CACTUS1, LARGE_CACTUS2, LARGE_CACTUS3]
        rnd_img = random.choice(cactus_img)
        self.img = rnd_img
        self.x = 500 - self.img.get_width() - 10
        self.y = 300 - self.img.get_height() - 10
    
    def slide(self):
        if self.x > -60:            
            self.x -= self.vel
        else:
            self.setCoord()
    
    def draw(self, win):
        win.blit(self.img, (self.x, self.y))


# Window
WIDTH = 500
HEIGHT = 300
WIN = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("Dino Adventure!")

# Colors
LIGHT_GRAY = (211, 211, 211)
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)

# Fonts
FONT = pygame.font.SysFont(None, 50)

# Game states
MENU = 0
GAME = 1
game_state = MENU

# Functions
def draw_menu():
    WIN.fill(LIGHT_GRAY)
    play_text = FONT.render("PLAY", True, BLACK)
    play_rect = play_text.get_rect(center=(WIDTH // 2, HEIGHT // 2))
    pygame.draw.rect(WIN, WHITE, play_rect.inflate(10, 10))
    WIN.blit(play_text, play_rect)

def draw_game():
    WIN.fill(LIGHT_GRAY)
    DINO.draw(WIN)
    CACTUS.draw(WIN)

def start_game():
    global game_state, DINO, CACTUS
    DINO = Dino()
    CACTUS = Cactus()
    game_state = GAME
    

# Main loop
clock = pygame.time.Clock()
run = True

# delay of 2 seconds
delay_timer = 120

while run:
    clock.tick(60)

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            run = False
        # it proceeds to check if the mouse click occurred within the boundaries of the "PLAY" button.
        elif event.type == pygame.MOUSEBUTTONDOWN:
            if game_state == MENU:
                mouse_pos = pygame.mouse.get_pos()
                play_rect = FONT.render("PLAY", True, BLACK).get_rect(center=(WIDTH // 2, HEIGHT // 2))
                if play_rect.collidepoint(mouse_pos):
                    start_game()

    if game_state == MENU:
        draw_menu()
    elif game_state == GAME:
        key_press = pygame.key.get_pressed()
        if DINO.isjumping == False:
            if key_press[pygame.K_RETURN] or key_press[pygame.K_SPACE]:
                DINO.isjumping = True

        DINO.jump()
        if delay_timer > 0:
            delay_timer -= 1  # Decrease the delay timer
        else:
            CACTUS.slide()
        draw_game()

    pygame.display.update()

pygame.quit()