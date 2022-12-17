# Username Generator

## Overview

This is a console application that randomly generates usernames in a title-case format (e.g. "WordWord"). Each with roughly-specific syllable lengths.

> Word data sourced from [EddyDN's JSON version](https://github.com/eddydn/DictionaryDatabase) of the public domain [Online Plain Text English Dictionary](https://www.mso.anu.edu.au/~ralph/OPTED/)

## Known Bugs

- English is a language that often doesn't play by a consistent set of phonetic rules, and as a result the count of syllables detected may be occasionally off by one based on the word endings.
