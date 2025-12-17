# 3D Fruit Ninja - Setup Instructions

Complete guide for setting up your 3D Fruit Ninja game in Unity.

## Prerequisites

- Unity Editor (2020.3 or later recommended)
- All scripts created in `Assets/Scripts/` folder

## Step-by-Step Setup

### 1. Camera Setup

1. Select the **Main Camera** in the Hierarchy
2. Set the camera position to see the play area (suggested position: `X=0, Y=5, Z=-15`)
3. Ensure the camera is tagged as **"MainCamera"**

### 2. Create the Blade GameObject

1. **Create the Blade:**
   - Right-click in Hierarchy → **3D Object → Sphere**
   - Rename to **"Blade"**
   - Set Scale to `(0.5, 0.5, 0.5)` for a smaller blade

2. **Add Components:**
   - Click **Add Component → Physics → Rigidbody**
     - ✅ Check **Is Kinematic**
     - ❌ Uncheck **Use Gravity**
   - Click **Add Component → Physics → Sphere Collider**
     - ✅ Check **Is Trigger**
   - Click **Add Component → Scripts → Blade** (drag the Blade.cs script)

3. **Configure Blade Script:**
   - Set **Blade Distance From Camera** to `10` (adjust based on your camera setup)
   - Set **Follow Speed** to `20` (higher = faster response)

### 3. Create Fruit Prefabs

#### Orange (Sphere)

1. **Create the Object:**
   - Right-click in Hierarchy → **3D Object → Sphere**
   - Rename to **"Orange"**
   - Set Scale to `(1, 1, 1)`

2. **Add Components:**
   - Click **Add Component → Physics → Rigidbody**
     - ✅ Check **Use Gravity**
     - Set **Mass** to `1`
   - Click **Add Component → Physics → Sphere Collider**
     - ❌ Uncheck **Is Trigger**
   - Click **Add Component → Scripts → Fruit** (drag the Fruit.cs script)

3. **Customize Appearance:**
   - Create a Material (Right-click in Project → **Create → Material**)
   - Name it **"Orange_Material"**
   - Set Albedo color to orange
   - Drag the material onto the Orange object

4. **Configure Fruit Script:**
   - Min Launch Force: `8`
   - Max Launch Force: `12`
   - Min Torque: `-50`
   - Max Torque: `50`
   - Destroy Y Position: `-10`

5. **Create Prefab:**
   - Drag the **Orange** object from Hierarchy to the **Project** window
   - Delete the Orange from the Hierarchy (we only need the prefab)

#### Watermelon (Capsule)

1. Repeat the same steps as Orange, but use **Capsule** instead of Sphere
2. Name it **"Watermelon"**
3. Use a green material
4. Create the prefab

### 4. Create the Spawner

1. **Create Empty GameObject:**
   - Right-click in Hierarchy → **Create Empty**
   - Rename to **"Spawner"**
   - Set Position to `(0, -5, 0)` (below the camera view)

2. **Add Spawner Script:**
   - Click **Add Component → Scripts → Fruit Spawner**

3. **Configure Spawner:**
   - Set **Spawn Interval** to `1.5` (seconds between spawns)
   - Set **Min X Position** to `-8`
   - Set **Max X Position** to `8`
   - Set **Min Launch Force** to `10`
   - Set **Max Launch Force** to `15`
   - Set **Fruit Prefabs** size to `2`
     - Drag **Orange** prefab to Element 0
     - Drag **Watermelon** prefab to Element 1

### 5. Create UI for Score

1. **Create Canvas:**
   - Right-click in Hierarchy → **UI → Canvas**
   - The Canvas and EventSystem will be created automatically

2. **Create Score Text:**
   - Right-click on Canvas → **UI → Text**
   - Rename to **"ScoreText"**
   - Configure:
     - Position: Top-left corner
     - Font Size: `32`
     - Color: White
     - Text: "Score: 0"
     - Alignment: Left

### 6. Create Game Manager

1. **Create Empty GameObject:**
   - Right-click in Hierarchy → **Create Empty**
   - Rename to **"GameManager"**

2. **Add Game Manager Script:**
   - Click **Add Component → Scripts → Game Manager**

3. **Assign Score Text:**
   - Drag the **ScoreText** object from Hierarchy to the **Score Text** field in GameManager

### 7. Configure Physics Settings (Important!)

1. Go to **Edit → Project Settings → Physics**
2. Ensure collision matrix allows:
   - **Default** layer collides with **Default** layer
3. If fruits aren't being sliced, check collision layers

## Testing Your Game

1. **Press Play** in Unity Editor
2. **Expected Behavior:**
   - Fruits spawn at random X positions from the spawner
   - Fruits launch upward and rotate
   - Blade follows your mouse cursor
   - When you move the blade over a fruit, it disappears (sliced)
   - Score increases by 1 for each sliced fruit
   - Fruits fall back down due to gravity and disappear when off-screen

## Troubleshooting

### Blade doesn't follow mouse
- ✅ Check that Main Camera is tagged as "MainCamera"
- ✅ Verify Blade has Rigidbody component
- ✅ Make sure Blade script is attached

### Blade doesn't slice fruits
- ✅ Ensure Blade's collider has **Is Trigger** checked
- ✅ Ensure Fruit's collider does **NOT** have **Is Trigger** checked
- ✅ Adjust **Blade Distance From Camera** to match your camera's Z position
- ✅ Check Physics collision matrix (Edit → Project Settings → Physics)

### Fruits don't spawn
- ✅ Check that fruit prefabs are assigned in Spawner
- ✅ Verify spawner position is below camera view
- ✅ Check Console for errors

### Fruits spawn but don't launch
- ✅ Ensure fruits have Rigidbody with **Use Gravity** checked
- ✅ Verify Fruit.cs script is attached to prefabs
- ✅ Check launch force values are not zero

### Score doesn't update
- ✅ Ensure ScoreText is assigned in GameManager
- ✅ Check that GameManager exists in the scene
- ✅ Verify UI Text component is correctly set up

## Advanced Customization

### Add Particle Effects
1. Create a Particle System (Right-click → **Effects → Particle System**)
2. Customize the effect (color, size, emission)
3. Create a prefab from it
4. Assign to **Slice Effect** field in Fruit script

### Adjust Difficulty
- **Easier:** Increase spawn interval, decrease launch force
- **Harder:** Decrease spawn interval, increase launch force, add more variation

### Add More Fruit Types
1. Create new 3D objects with different shapes
2. Add Rigidbody, Collider, and Fruit script
3. Customize materials and colors
4. Create prefabs
5. Add to Spawner's Fruit Prefabs array

## Next Steps

Once your game is working:
- Add bomb objects that decrease score
- Implement game over conditions (3 missed fruits)
- Add sound effects for slicing
- Create a main menu
- Add difficulty progression
- Implement a high score system

---

**Good luck with your university project! 🎮🍉**
