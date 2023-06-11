import pygame
import random

# Initialize Pygame
pygame.init()

# Window
screen = pygame.display.set_mode((800, 600))
pygame.display.set_caption("Dino Game")
clock = pygame.time.Clock()

# Load Assets
dino_img = pygame.image.load("assets/dino_1.png")
cactus_img = pygame.image.load("assets/cacti.png")
ground_img = pygame.image.load("assets/ground.png")
hit_img = pygame.image.load("assets/hit.png")
beep_sound = pygame.mixer.Sound("assets/beep.wav")

# Dino
dino_rect = dino_img.get_rect()
dino_rect.x = 100
dino_rect.y = 300
dino_y_velocity = 0

# Ground
ground_rect1 = ground_img.get_rect()
ground_rect1.x = 0
ground_rect1.y = 550

ground_rect2 = ground_img.get_rect()
ground_rect2.x = 800
ground_rect2.y = 550

# Cacti
cacti = []
cactus_rect = cactus_img.get_rect()
cactus_rect.x = 800
cactus_rect.y = 300

# Points
points = 0
font = pygame.font.Font(None, 40)
text = font.render(f"Points: {points}", True, (0, 0, 0))
text_rect = text.get_rect()
text_rect.topleft = (10, 10)

# Game Loop
running = True
while running:
    # Handle Events
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
        elif event.type == pygame.KEYDOWN:
            if event.key == pygame.K_SPACE and dino_rect.y >= 300:
                dino_y_velocity = -22
                beep_sound.play()

    # Update
    dino_y_velocity += 1
    dino_rect.y += dino_y_velocity

    if dino_rect.y >= 300:
        dino_rect.y = 300

    ground_rect1.x -= 6
    ground_rect2.x -= 6

    if ground_rect1.x <= -800:
        ground_rect1.x = 0

    if ground_rect2.x <= 0:
        ground_rect2.x = 800

    if len(cacti) == 0 or cacti[-1].x <= 400:
        new_cactus = cactus_img.get_rect()
        new_cactus.x = 800
        new_cactus.y = 325
        cacti.append(new_cactus)

    for cactus in cacti:
        cactus.x -= 6
        if cactus.colliderect(dino_rect):
            dino_img = hit_img
            running = False

    points += 1
    text = font.render(f"Points: {points}", True, (0, 0, 0))

    # Render
    screen.fill((200, 200, 200))
    screen.blit(dino_img, dino_rect)
    screen.blit(ground_img, ground_rect1)
    screen.blit(ground_img, ground_rect2)
    for cactus in cacti:
        screen.blit(cactus_img, cactus)
    screen.blit(text, text_rect)

    pygame.display.flip()
    clock.tick(60)

# Quit Pygame
pygame.quit()
