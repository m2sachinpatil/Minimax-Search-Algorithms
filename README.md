
# Rover Movement

A brief description of what this project does and who it's for

SPECIFICATION
A squad of robotic rovers are to be landed by NASA on a plateau on Mars. The navigation team needs a
utility for them to simulate rover movements so they can develop a navigation plan.

A rover&#39;s position is represented by a combination of an x and y co-ordinates and a letter
representing one of the four cardinal compass points. The plateau is divided up into a grid to
simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom
left corner and facing North.

In order to control a rover, NASA sends a simple string of letters. The possible letters are:

> &#39;L&#39; – Make the rover spin 90 degrees left without moving from its current spot

> &#39;R&#39; - Make the rover spin 90 degrees right without moving from its current spot

> &#39;M&#39;. Move forward one grid point, and maintain the same heading.

Assume that the square directly North from (x, y) is (x, y+1).

APPLICATION

Your application should be a Console Application written in C#. All input will be entered by the User in
real-time. All output should be written directly to the Console window via Standard out.

INPUT

The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are
assumed to be 0,0.

The rest of the input is information pertaining to the rovers that have been deployed. Each rover
has two lines of input. The first line gives the rover&#39;s position, and the second line is a series of
instructions telling the rover how to explore the plateau.

The position is made up of two integers and a letter separated by spaces, corresponding to the x
and y co-ordinates and the rover&#39;s orientation.

Each rover will be finished sequentially, which means that the second rover won&#39;t start to move
until the first one has finished moving.

OUTPUT

The output for each rover should be its final co-ordinates and heading.

Example Program Flow:

Enter Graph Upper Right Coordinate: 5 5

Rover 1 Starting Position: 1 2 N

Rover 1 Movement Plan: LMLMLMLMM

Rover 1 Output: 1 3 N

Rover 2 Starting Position: 3 3 E

Rover 2 Movement Plan: MMRMMRMRRM

Rover 2 Output: 5 1 E
