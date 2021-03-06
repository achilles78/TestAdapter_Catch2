# How to build Test Adapter for Catch2

The easiest way to build the **Test Adapter for Catch2** yourself is to open the Visual Studio solution file (VSTestAdapterCatch2.sln) and build the 'VSTestAdapterCatch2'-project. By default, this builds an unsigned vsix-file that can be used to install the test adapter in Visual Studio. Be sure to set the Solution Configuration to `Release` if you intend to use the generated vsix-file to install the test adapter.

## Creating a signed vsix-file

If you need the generated assembly (vsix-file) to be digitally signed you need to add a key-file, named "key.snk" in the root of the 'VSTestAdapterCatch2'-project folder. When you then reload the project this key-file will be used to sign the assembly.

## atom.xml

For those that want to add the test adapter to their own extension repository, below is an excerpt of the entry that can be placed in your "atom.xml"-file.

``` xml
  <entry>
    <id>VSTestAdapterCatch2.73ba8471-3771-47bf-bd23-49a1ba09af89</id>
    <title type="text">Test Adapter for Catch2.</title>
    <summary type="text">Visual Studio Test Adaptor for use with Catch2 test framework.</summary>
    <published>2018-06-24T12:17:00Z</published>
    <updated>2018-06-24T12:17:00Z</updated>
    <author>
      <name>Johnny Hendriks</name>
    </author>
    <content type="application/octet-stream" src="VSTestAdapterCatch2.vsix" />
    <Vsix xmlns:xsd="http://www.w3.org/2001/XMLSchema"
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          xmlns="http://schemas.microsoft.com/developer/vsx-syndication-schema/2010">  
      <Id>VSTestAdapterCatch2.73ba8471-3771-47bf-bd23-49a1ba09af89</Id>
      <Version>1.0.0</Version>
      <References />
      <Rating xsi:nil="true" />
      <RatingCount xsi:nil="true" />
      <DownloadCount xsi:nil="true" />
    </Vsix>
  </entry>
```