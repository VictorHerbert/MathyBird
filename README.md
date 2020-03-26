# MathyBird
[![Open In Colab](https://colab.research.google.com/assets/colab-badge.svg)](https://colab.research.google.com/github/weiji14/deepbedmap/]

AI controlled FlappyBird

Base material in [FlappyBird](https://github.com/VictorHerbert/FlappyBird/)

## Features

* Control systems
* Machine Learning


## Control systems

Let F(t) be the force aplied over time to the bird. 

<img src="https://latex.codecogs.com/gif.latex?F(t)&space;=&space;g&space;m&space;&plus;&space;\delta(t-T)" title="F(t) = \alpha m + \delta(t-T)" />


<img src="https://latex.codecogs.com/gif.latex?\alpha(t)&space;=&space;g&space;&plus;&space;\frac{1}{m}&space;\delta(t-T)" title="\alpha(t) = g + \frac{1}{m} \delta(t-T)" />


<img src="https://latex.codecogs.com/gif.latex?v(t)&space;=&space;\int_0^t&space;g&space;&plus;&space;\frac{1}{m}&space;\delta(t-T)&space;\quad&space;dt&space;=&space;gt&space;&plus;&space;\frac{1}{m}&space;u(t-T)" title="v(t) = \int_0^t g + \frac{1}{m} \delta(t-T) \quad dt = gt + \frac{1}{m} u(t-T)" />

