# Character type constants
LOWER = list("abcdefghijklmnopqrstuvwxyz")
UPPER = list("ABCDEFGHIJKLMNOPQRSTUVWXYZ")
DIGITS = list("0123456789")
SPECIAL = list("!@#$%^&*()-_=+[]{}|;:\"\",.<>?/~`")

# Function to check if word is in a file
def word_in_file(word, filename, case_sensitive=False):
    try:
        with open(filename, "r", encoding="utf-8") as file:
            for line in file:
                file_word = line.strip()
                if case_sensitive:
                    if word == file_word:
                        return True
                else:
                    if word.lower() == file_word.lower():
                        return True
    except FileNotFoundError:
        print(f"Error: File {filename} not found.")
    return False

# Function to check if word has at least one character from a list
def word_has_character(word, character_list):
    for char in word:
        if char in character_list:
            return True
    return False

# Function to calculate word complexity (0-4)
def word_complexity(word):
    complexity = 0
    if word_has_character(word, LOWER):
        complexity += 1
    if word_has_character(word, UPPER):
        complexity += 1
    if word_has_character(word, DIGITS):
        complexity += 1
    if word_has_character(word, SPECIAL):
        complexity += 1
    return complexity

# Main password strength logic
def password_strength(password, min_length=10, strong_length=16):
    if word_in_file(password, "dictionary.txt", case_sensitive=False):
        print("Password is a dictionary word and is not secure.")
        return 0

    if word_in_file(password, "top_passwords.txt", case_sensitive=True):
        print("Password is a commonly used password and is not secure.")
        return 0

    if len(password) < min_length:
        print("Password is too short and is not secure.")
        return 1

    if len(password) >= strong_length:
        print("Password is long, length trumps complexity this is a good password.")
        return 5

    complexity = word_complexity(password)
    strength = 1 + complexity
    print(f"Password complexity score: {complexity}")
    return strength

# User input loop
def main():
    while True:
        password = input("Enter a password to check (or 'q' to quit): ")
        if password.lower() == 'q':
            break
        score = password_strength(password)
        print(f"Password strength: {score} (0=weak, 5=strong)\n")

# Run only if this file is the entry point
if __name__ == "__main__":
    main()
