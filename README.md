# Bencode to JSON converter

`bencode2json` command-line tool converts a [bencode](http://en.wikipedia.org/wiki/Bencode) file to a [JSON](http://en.wikipedia.org/wiki/JSON) file.

Written in pure C# and does NOT use any external libraries like JSON.NET.

This tool is part of [`dostools` collection](https://github.com/vurdalakov/dostools).

##### Syntax

```
bencode2json bencode_file [json_file] [-silent]
```

If JSON file name is not specified, tool creates JSON file in the same directory and with the same name as bencode file, but with `.json` extension.

`-silent` option tells tool not to print anything to stdout; check exit code for result.

##### Exit codes

  * 0 - file conversion succeeded;
  * 1 - file conversion failed;
  * -1 - invalid command line syntax.

##### License

Copyright © 2015 [Vurdalakov](http://www.vurdalakov.net).

Project is distributed under the [MIT license](http://opensource.org/licenses/MIT).
