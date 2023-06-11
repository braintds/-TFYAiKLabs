# Lab 3 
Develop a lexical analyzer that allows you to highlight lexemes in the text, consider other characters invalid (display an error).<br>
Input data - string (text of program code).<br>

The output data is a sequence of conditional codes that describe the structure of the parsed text, indicating the position and type ("number", "identifier", "sign", "invalid character", etc.). For example, for the string "int x=123;":<br>
14 - keyword - int - character 1 to 3<br>
11 - separator - (space) - 4th to 4th character<br>
2 - identifier - x - 5th to 5th character<br>
10 - assignment operator - = - from 6th to 6th character<br>
1 - unsigned integer - 123 - 7th to 9th character<br>
16 - end of operator - ; - 10th to 10th character<br><br>

Python tokens<br>
Identifier (letter {letter | digit});<br>
Assignment operator (=), colon (:) and<br>
whitespace characters;<br>
String, CBZ (digit {digit}) and real number<br>
(digit{digit}[“.”{digit} digit].);<br>
Keywords: while, for, if, else, break;<br>
Invalid character<br><br>

# Лабораторная 3 
Разработать лексический анализатор, позволяющий выделить в тексте лексемы, иные символы считать недопустимыми (выводить ошибку)<br>
Входные данные - строка (текст программного кода).<br>

Выходные данные - последовательность условных кодов, описывающих структуру разбираемого текста с указанием места положения и типа ("число", "идентификатор", "знак", "недопустимый символ" и т.д.). Например, для строки "int x=123;":<br>
14 - ключевое слово - int - с 1 по 3 символ<br>
11 - разделитель - (пробел) - с 4 по 4 символ<br>
2 - идентификатор - x - с 5 по 5 символ<br>
10 - оператор присваивания - = - с 6 по 6 символ<br>
1 - целое без знака - 123 - с 7 по 9 символ<br>
16 - конец оператора - ; - с 10 по 10 символ<br><br>

Лексемы Python<br>
Идентификатор (letter {letter | digit});<br>
Оператор присваивания (=), двоеточие (:)и пробельные символы;<br>
Строка, ЦБЗ (digit {digit}) и вещественноечисло<br>
(digit{digit}[“.”{digit} digit].);<br>
Ключевые слова: while, for, if, else, break;<br>
Недопустимый символ<br>
