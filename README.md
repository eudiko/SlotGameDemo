**Slot Machine Game**

A simple 3-reel slot machine built in Unity. It uses UI-based reels, weighted symbol generation, a gold betting system, and audio feedback.

**Game Overview**

The game has three reels that spin independently and stop one after another with a small bounce animation using DOTween.
There are four symbols: Seven, Cherry, Bell, and Bar. Each symbol has its own weight and payout value defined using ScriptableObjects.
Players can choose a bet of 10, 20, or 50 gold before spinning. If the player does not have enough gold, the corresponding buttons are disabled.
A win happens when all three reels land on the same symbol. The payout is calculated as:
payout = bet × symbol multiplier
Audio includes a looping spin sound, a stop sound for each reel, and different sounds for win and lose outcomes.
Input can be given either by clicking the Spin button or pressing the Space key.

**How to Run (WebGL Build)**
Download or clone the repository
Open Unity Project and use the build and run option
OR
Open a terminal in the WebGL Build folder
Start a local server and run it there.

**Architecture**

Each system is separated by responsibility:

SlotMachine: Controls the full spin flow using a coroutine
SlotAnimation: Handles reel movement and stopping animation
SymbolGenerator: Selects results using weighted randomness
BettingManager: Manages gold, bets, and payouts
AudioManager: Plays and manages all sound effects
SymbolData: ScriptableObject storing symbol data like sprite, weight, and multiplier

**Design Approach**

The result is generated before the reels stop. This keeps game logic separate from visuals and avoids timing issues.
Symbol probabilities are controlled through weights in ScriptableObjects, so balancing can be done without changing code.
DOTween is used to create a slight overshoot and bounce when reels stop, making the animation feel more responsive.
Singletons are used for managers to simplify access in a single-scene setup.
Coroutines are used to control the sequence of reel stopping, creating a staggered effect.
