# Technical Design GPS

This is the technical design document for the GPS system, where I planned on searching up on how to get your current location and the target location. 

## Wanted Functions

A system that is able to detect where you are in the world by using the GPS location and Geo location, then assign them to game objects in the game scene so it matches the locations in real map.  
  
## Searching  
  
I started searching on how to make a system for unity that would track your location, I would be directed to a few videos that are using MapBox, so I would see what it's about, turns out that it's a package that can be installed on unity, that uses the google maps API, making it easier to access it in Unity for any developer that is planning on making a game with a GPS system.  
  
When searching on how to use it and make it so it's possible to find the distance between the player and the location in real time, so it can trigger scripts when reaching the location. I found out that MapBox didn't have it built in, so I had to download an extension script that decoded the Geolocation of the coordinate of the player and target location.     
  
As I searched I could find different ways of doing the whole getting the GPS system, but do to not having much time to work on this, I didn't have a lot of time to search for many options, so I found the quickest option that I could find to get what we wanted to be made be made. 
  
## Experimenting  
  
Using the MaxBox example scenes I would see to create a copy of location that we are making the game for, MapBox was meant to create a map background according to the location that was set using coordinates of google maps, for me it didn't want to work exactly how it should of been, so I figured out how to turn off that function.  
  
Seeing how some of the scripts of the scene weren't part of the package, but they were part of the example, and needed for the whole system to work, so I started to see what was needed and copied it, seeing what is used and making my own version to be able to run it for myself later.  
  
