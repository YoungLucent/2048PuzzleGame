2048 Puzzle Game

2048 Puzzle Game is a hyper-casual puzzle game developed in Unity 6.

It reimagines the classic 2048 mechanics by adding a 3D physics engine. Players must aim and shoot cubes to merge numbers, managing space and physics interactions to reach the ultimate goal.
🧩 Overview

    Engine: Unity 6000.0.x (Android Support)

    Platform: Android / PC (Editor)

    Genre: Hyper-Casual / Physics Puzzle

    Language: C#

    Status: Release v1.0

🎮 Core Features

    Physics-Based Gameplay: Cubes collide, tumble, and stack dynamically using Unity's Rigidbody system.

    Aim & Shoot Mechanic: Intuitive touch controls to launch cubes into the arena.

    Merge Logic: Classic 2048 progression (2→4→8⋯→2048) with visual feedback.

    Game Loop:

        Win Condition: Merge cubes to reach the number 2048.

        Lose Condition: The arena overflows (cubes cross the dead zone line).

    Juicy Visuals: Smooth animations powered by DOTween (bounce, scale, fade effects).

    Dynamic Audio: Sound system with random pitch variation for satisfying merge effects.

🧠 Patterns & Architecture

    Observer Pattern: Decoupled architecture using static events (GameEvents.OnCubeMerged, GameEvents.OnGameOver) to communicate between systems without direct dependencies.

    State Management: Clear separation between Playing, Game Over, and Victory states.

    Clean Code Principles:

        CubeController: Handles individual cube logic and collisions.

        SoundManager: Independent audio system listening to events.

        UIController: Handles menus and HUD, completely separated from game logic.

    Mobile Optimization: Optimized for ARM64 architecture with efficient physics handling.

🛠️ Technologies & Tools

    Unity 6 (IL2CPP, Android Build Support)

    C#

    DOTween (Animation Engine)

    LeanPool (Object Pooling)

    Git & GitHub (Version Control)

    Visual Studio
