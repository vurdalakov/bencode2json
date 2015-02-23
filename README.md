# Bencode to JSON converter

`bencode2json` command-line tool converts a [bencode](http://en.wikipedia.org/wiki/Bencode) file to a [JSON](http://en.wikipedia.org/wiki/JSON) file.

Written in pure C#, does not use any 3-rd party libraries like JSON.NET.

##### Syntax

```
bencode2json file1 file2 [-silent]
```

`-silent` option tells tool not to print anything to stdout; check exit code for result.

##### Exit codes

  * 0 - file conversion succeeded;
  * 1 - file conversion failed;
  * -1 - invalid command line syntax.

##### License

Copyright © 2015 [Vurdalakov](http://www.vurdalakov.net).

Project is distributed under the [MIT license](http://opensource.org/licenses/MIT).
