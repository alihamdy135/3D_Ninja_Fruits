# 🍉 Ninga Fruits 3D

> An interactive 3D arcade game developed using Unity Engine, demonstrating core Computer Graphics concepts, physics simulation, and dynamic gameplay mechanics.

![Unity](https://img.shields.io/badge/Unity-2022.3%20LTS-black?style=flat&logo=unity)
![Language](https://img.shields.io/badge/Language-C%23-blue?style=flat&logo=csharp)
![Course](https://img.shields.io/badge/Course-CSC--317-orange)

## 🎮 Project Overview
**Dojo Slicer 3D** is a final project for the **CSC-317 Computer Graphics** course. It simulates a ninja training session where players must slice fruit thrown into the air while avoiding explosives. The project focuses on implementing 3D transformations, lighting models, texture mapping, and vector mathematics within a game engine environment.

## ✨ Key Features
* **3D Physics Simulation:** Fruits are instantiated with randomized impulse forces and torque for realistic projectile motion.
* **Slicing Mechanic:** Uses Raycasting and vector velocity tracking to simulate cutting interactions in 3D space.
* **Dynamic Difficulty:** An algorithmic difficulty ramp that increases spawn speed and bomb probability every 10 points.
* **Parallax Camera Effect:** Dynamic camera movement based on mouse input to enhance depth perception.
* **Visual Effects:** Particle systems for juice splashes, explosions, and trail rendering for the blade.
* **Audio System:** Adaptive background music and spatial sound effects (SFX) for interactions.

## 🛠️ Technical Implementation

### 1. Graphics & Math
* **Coordinate Systems:** Implemented `Camera.ScreenToWorldPoint` to map 2D mouse inputs to the 3D game world.
* **Lighting (Phong Model):** Utilized Directional Lighting with Specular Highlights on fruit materials to simulate realistic surface reflection.
* **Parallax:** Applied matrix transformations to the camera's rotation based on Normalized Device Coordinates (NDC) of the mouse.

### 2. Architecture & Patterns
* **Singleton Pattern:** Used in `GameManager` to maintain a single global state for score, lives, and audio.
* **Coroutines:** Used in `Spawner` for non-blocking, precise timing of object instantiation.
* **Object Pooling Strategy:** Efficient memory management by destroying off-screen objects (`Destroy(gameObject, 5f)`).

## 🚀 How to Play
1.  **Launch the Game:** Run the `DojoSlicer3D.exe` file.
2.  **Controls:** Move the mouse to control the blade.
3.  **Objective:** Slice the fruits to gain points (+1 per fruit).
4.  **Hazards:**
    * ❌ Do not drop fruits (You have 3 Hearts).
    * 💣 Do not slice Bombs (Instant Game Over).
5.  **Restart:** The game automatically offers a restart option upon Game Over.

## 📸 Screenshots
*(Please upload your screenshots to a folder named 'Screenshots' in your repo and link them here)*

| Gameplay Action | Game Over Screen |
|:---:|:---:|
| ![Gameplay](Screenshots/gameplay.png) | ![GameOver](Screenshots/gameover.png) |

## 📂 Project Structure
```text
Assets/
├── Scripts/
│   ├── GameManager.cs   # Core Logic (Score, Lives, Audio)
│   ├── Blade.cs         # Input Handling & Raycasting
│   ├── Spawner.cs       # Object Instantiation & Difficulty Ramp
│   ├── Fruit.cs         # Collision & Mesh Swapping
│   ├── CameraParallax.cs# 3D Depth Effect
│   └── ...
├── Prefabs/             # Pre-configured GameObjects (Fruits, Bombs)
├── Materials/           # Textures & Shaders
└── Scenes/              # Main Scene
