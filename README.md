# Psiket-QPong: Quantum Version of Pong Game

Psiket-QPong is a unique quantum-inspired version of the classic Pong game, developed for the PsiKet Academy. The game incorporates quantum mechanics principles to create a novel and challenging gameplay experience.

## Features

- **Configurable Qubits**: Players can set the number of qubits (2 or 3) used in the game, for both Singleplayer and Multiplayer modes.
- **AI Strength**: The strength of the AI opponent can be adjusted.
- **Ball Speed**: The speed of the quantum ball can be set to different levels.
- **Game Modes**:
  - **Singleplayer**: The player controls the left paddle, while the right paddle is controlled by a quantum-based AI.
  - **Multiplayer**: Two players can play against each other using a shared keyboard.

## Gameplay

### Singleplayer

In the singleplayer mode, the player can use either the W/A/S/D/Z/X controls (left hand) or the Left/Right/Up/Down/1/2 controls (right hand) to interact with the qubits and control the left paddle.

The right paddle is controlled by a quantum-based AI, which determines its position based on the state of the qubits. The player can influence the right paddle's position by placing the following gates on the qubits:

- **X Gate**: Flips the state of the selected qubit from 0 to 1, or vice versa.
- **Hadamard Gate**: Places the qubit in a superposition of 0 and 1, with a 50% probability of each state.

The resulting qubit state determines the position of the right paddle:

- **2 Qubits**: The right paddle can be in one of four states (00, 01, 10, 11), based on the qubit values.
- **3 Qubits**: The right paddle can be in one of eight states (000, 001, 010, 011, 100, 101, 110, 111), based on the qubit values.

The player must strategically place gates on the qubits to influence the position of the right paddle and successfully return the quantum ball.

### Multiplayer

In the multiplayer mode, two players can compete against each other using a shared keyboard:

- **Left Player**: W, A, S, D, Z (place Hadamard gate), X (place X gate)
- **Right Player**: Left, Right, Up, Down, 1 (place Hadamard gate), 2 (place X gate)

The players take turns placing gates on the qubits to control the position of their respective paddles and return the quantum ball. The number of qubits (2 or 3) can be set for the multiplayer mode as well.

## Installation and Setup

1. Clone the repository from GitHub: `https://github.com/niloufarmj/Psiket-QPong.git`
2. Open the project in Unity.
3. Build and run the game.

## Contributing

If you would like to contribute to the development of Psiket-QPong, please follow these guidelines:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Implement your changes and test them thoroughly.
4. Submit a pull request with a detailed description of your changes.

## License

This project is licensed under the MIT License.
