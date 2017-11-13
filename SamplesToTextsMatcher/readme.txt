Main work algorythm of library:
1. User creates a pattern using avaliable non-terminal expressions (the list is below) and terms (simple words)
2. The pattern is cleaned from double spaces etc.
3. Pattern is parsed for linked list of expressions.
4. Linked list is modified to reverse Polish notation of expressions presentation
5. Syntax binary tree of expressions is created.
--- String/pattern matching---
6. String is cleaned
7. String is tokenized. Positions of tokens are incapsulated in tokens objects.
8. Each synax tree's node is resolved recursively. The result of resolving of the node is a boolean value.
8. The result of matching of string and pattern is boolean value of resolved tree's root. 


Avaliable non-terminal expressions:
1. | - or operator. Usage: word1 | word2
2. & - and operator. Usage: word1 & word2
3. ~ - not operator. Usage: word1 ~word2
4. "" - quotes for terminal or several terminals. Usage: "word1 word2". OR: ="word1 word2"
5. * - asterix for getting all forms of terminal part before asterix sign. 
Forms are got from morphological dictionary. Usage: word*  - it will return (word1 | word2 |word3) if they are in morphological dictionary.
6. /n - maximum distance between 2 expressions. E.g. word1 /5 word2
7. = - Equals sign. Usage =word1 OR ="word1 word2"
8. () - Brackets.

Example of pattern:
(((word1 | word2) /5 word3) & (word4 | word5*) ~word6) | ="word7 word8"