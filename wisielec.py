#Gra Wisielec  klasy WordBank, Game, Player; losowanie słowa, odsłanianie liter, limit błędów, stan gry.

import random

with open('WordBank.txt') as file:
    words = [line.strip() for line in file]

random_word = random.choice(words)
print("Słowo to " + random_word)

for x in random_word:
  print(x + " ")
