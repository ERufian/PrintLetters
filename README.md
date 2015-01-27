PrintLetters
============

Print mnemonic combinations of 10-digit telephone numbers. These are combinations of letters and numbers 
that are easier to remember than 10 digits (using letters as shown on a telephone keyboard).  

The portions that remain as numbers are:  
* The country and area codes if they are of the form 1-800 or 1-888  
* The digits 1 and 0  

The remaining digits are converted to letters and all the possible combinations are printed out.  

Some time ago ago I was asked this question at a phone interview. I overcomplicated the answer and didn't get anywhere, 
which was tremendously annoying. After the interview I decided to write it out as an exercise.  

The result was interesting and I thought that maybe someone actually has uses for this kind of functionality,
so I decided to upload it to Github.  

Some time later, on an interview for a different company I was challenged to optimize my old code and come up with a 
more efficient solution that didn't require using a stack (implicitly, as recursion does or explicitly). The original 
interviewer was guiding me towards a recursive solution, which is what I first implemented. Recursion is interesting 
from a theoretical point of view and often used in interviews to see if the candidate can think recursively, however
when moving from theory to practice sometimes it can lead to surprisingly inefficient code. Because of the function
calls and all the stack manipulation, if there is a non-recursive solution _of similar complexity_ it is often faster.  

This problem can be solved with a straight loop. We can count down in base 4 and have a set of precomputed values for
"cascading" the count (when the counter rolls over and goes to binary 11, reset it to a lower number as needed). 
The resulting solution uses only addition, subtraction and bit manipulation. There is no need for multiplication
division or modulo operations (multiplications by powers of 2 have been replaced by bit shifts).  

It was very interesting as an exercise in optimization, although some degree of maintainability had to be sacrificed
in the process.

Note: the original recursive solution is still available, switch to the "RecursiveSolution" branch if you're curious).  

Feel free to re-use any of this code. I am releasing it under a choice of MIT License or GPL v2 (your choice, pick one 
or the other). If you need different terms please let me know.  
