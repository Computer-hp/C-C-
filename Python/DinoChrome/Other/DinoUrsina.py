from ursina import *
import random as r

# Window
app = Ursina()
window.fullscreen = True
window.color = color.light_gray

#  Dino
     # Animation is used to make move the dino legs
# dino because we use every png named dino
# collider is used to define the shape of objects in a scene 
# and determine how they interact with other objects in terms of collisions.

dino = Animation('assets\dino',
                 collider = 'box',
                 x = -5,
                 y = 0)

ground1 = Entity(
  model = 'quad',
  texture = 'assets\ground',
  scale = (50, 0.5, 1),
  z = 1
)   
ground2 = duplicate(ground1, x=50)
pair = [ground1, ground2]


# The model parameter is used to specify the visual 
# representation or shape of the Entity object.
cactus = Entity(
  model='quad',
  texture = 'assets\cacti',
  x = 20,
  y = 0,
  collider = 'box',
  # set the size of the model and collider
  scale = (0.5, 1, 0)
)
# here we store the x position of the new cactus
cacti = []
def newCactus():
  # creates a new cactus object by duplicating the object cactus
  new = duplicate(cactus,
                  x = 12 + r.randint(0,5))
  cacti.append(new)
  # invokea the newCactus() function again after 2 milliseconds
  invoke(newCactus, delay = 2)

newCactus()



label = Text(
  text = f'Points: {0}',
  color = color.black,
  position = (-0.5, 0.4),
  scale_x = 3,
  scale_y = 2
)
points = 0

def update():
  global points
  points += 1
  label.text = f'Points: {points}'

  for ground in pair:
    ground.x -= 6 * time.dt
    if ground.x < -35:
      ground.x += 100
  for c in cacti:
    c.x -= 6 * time.dt
  if dino.intersects().hit:
    dino.texture= 'assets\hit'
    application.pause()


sound = Audio(
  'assets\\beep',
  autoplay = False
)


def input(key):
  if key == 'space':
    if dino.y < 0.01:
      sound.play()
      # move up
      dino.animate_y(
        2,
        duration = 0.4,
        # out_sine curve for easing, which means the animation will start slowly and then accelerate
        curve= curve.out_sine
      )
      # move down
      dino.animate_y(
        0,
        duration = 0.4,
        # The delay ensures that the dinosaur stays in 
        # the air for a brief moment before descending.
        delay = 0.4,
        curve = curve.in_sine
      )

camera.orthographic = True  # To have no perspective
camera.fov = 10

app.run()