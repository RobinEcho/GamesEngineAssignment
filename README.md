Name: Junjie Peng

Student Number: D18124895

Class Group: dt228

# Music Tunnel

# Description of the project

This Project will create a tunnel

User will move insdie this tunnel which will create tones of stuff like cube on UI

tunnel itself holds color and will change through time and music

# Instructions for use

Run the process, it will create a 3D tunnel, the speed of drawing tuneel is base on amplitude of music.

Press space to unlock the camera which allows user trun around to view the tunnel. 

Use right side slider to change the style of Tunnel

# How it works

1. Signed audio resource object, collect audio data by unity method GetSpectrumData, signed these data into band array

2. get dynamic array with audio spectrum data from above.

3. To draw tunnel, its actually draw a circule but with trailRenderer component, it allows us to keep the trail when point move to next position. so we get a 2D image

4. Push this surface forward and keep drawing, it looks like 3D tunnel right now.


# List of classes/assets in the project

| Class/asset | Source |
|-----------|-----------|
| AudioCollecter.cs | Self written |
| AudioSlider.cs | Self written |
| CameraController.cs | Self written |
| CubeAnimator.cs | Self written |
| CubeGenerator.cs | Self written |
| TrailGenerator.cs | Self written |
| WallGenerator.cs | From [Procedural Phyllotaxis](https://www.youtube.com/watch?v=PwHANpTc87E&t=146s) |

# What I am most proud of in the assignment

* Everything works as expected, all scripts looks clean and easy to understand due to well comment.
* OO used and followed code design pattern
* Understand all script and component
* Visual style consistent
* Implment 3D visual by samplest way


# Demo Link
[Youtube Link](https://www.youtube.com/watch?v=xlx7BKFrUAs)
