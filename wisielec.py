import random

class WordBank:
    def __init__(self, filename):
        with open(filename, 'r') as file:
            self.words = [line.strip().lower() for line in file]

    def get_random_word(self):
        return random.choice(self.words)


class Player:
    def __init__(self, word):
        self.word = word
        self.guessed_letters = set()
        self.errors = 0
        self.max_errors = 6

    def guess(self, letter):
        letter = letter.lower()
        if letter in self.guessed_letters:
            return False
        self.guessed_letters.add(letter)
        if letter not in self.word:
            self.errors += 1
            return False
        return True

    def display_word(self):
        return ' '.join([x if x in self.guessed_letters else '_' for x in self.word])

    def has_won(self):
        return all(x in self.guessed_letters for x in self.word)

    def has_lost(self):
        return self.errors >= self.max_errors


class Game:
    def __init__(self, wordbank_file):
        self.wordbank = WordBank(wordbank_file)
        self.random_word = self.wordbank.get_random_word()
        self.player = Player(self.random_word)

    def draw_hangman(self):
        stages = [
            "",
            "\n\n\n\n\n_|_",

            "\n |\n |\n |\n |\n_|_",

            " |===|\n |\n |\n |\n_|_",

            " |===|\n |   0\n |\n |\n_|_",

            " |===|\n |   0\n |  /|\\\n |\n_|_",

            " |===|\n |   0\n |  /|\\\n |   /\\\n_|_"
        ]
        print(stages[self.player.errors])

    def play(self):
        print("Witaj w grze Wisielec!")

        while not self.player.has_won() and not self.player.has_lost():
            self.draw_hangman()
            print("Słowo:", self.player.display_word())
            print(f"Błędy: {self.player.errors}/{self.player.max_errors}")
            guess = input("Podaj literę: ").strip().lower()

            if self.player.guess(guess):
                print("Dobrze!")
            else:
                print("Źle!")

        self.draw_hangman()
        if self.player.has_won():
            print(f"\nBrawo! Odgadłeś słowo: {self.random_word}")
        else:
            print(f"\nPrzegrałeś! Słowo to: {self.random_word}")


if __name__ == "__main__":
    gra = Game('WordBank.txt')
    gra.play()
