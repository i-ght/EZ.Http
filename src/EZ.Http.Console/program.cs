using System.Linq;
using EZ.Http;

var ezs = new Queue<CURL>();
for (var i = 0; i < 100; i++) {
    ezs.Enqueue(CurlEz.Alloc());
}

await using var conductor = 
    new EZHttpConductor(
        acuireCURL: () => ezs.Dequeue(),
        freeCURL: ez => ezs.Enqueue(ez),
        connectCfg: new(
            MaxKeepAlives: 4096,
            MaxKeepAlivesPerHost: 8,
            MaxTotalConnections: 8192
        )
    );

var uris = new Queue<string>(new List<string> {
    "https://en.wikipedia.org/",
    "https://youtube.com/",
    "https://amazon.com/",
    "https://twitter.com/",
    "https://fandom.com/",
    "https://pinterest.com/",
    "https://imdb.com/",
    "https://reddit.com/",
    "https://yelp.com/",
    "https://instagram.com/",
    "https://ebay.com/",
    "https://walmart.com/",
    "https://healthline.com/",
    "https://linkedin.com/",
    "https://webmd.com/",
    "https://netflix.com/",
    "https://apple.com/",
    "https://homedepot.com/",
    "https://mail.yahoo.com/",
    "https://cnn.com/",
    "https://etsy.com/",
    "https://google.com/",
    "https://yahoo.com/",
    "https://indeed.com/",
    "https://target.com/",
    "https://nytimes.com/",
    "https://mayoclinic.org/",
    "https://espn.com/",
    "https://gamepedia.com/",
    "https://irs.gov/",
    "https://nih.gov/",
    "https://merriam-webster.com/",
    "https://steampowered.com/",
    "https://mapquest.com/",
    "https://foxnews.com/",
    "https://allrecipes.com/",
    "https://quora.com/",
    "https://aol.com/",
    "https://britannica.com/",
    "https://live.com/",
    "https://rottentomatoes.com/",
    "https://ca.gov/",
    "https://play.google.com/",
    "https://cnet.com/",
    "https://roblox.com/",
    "https://zillow.com/",
    "https://businessinsider.com/",
    "https://bulbagarden.net/",
    "https://finance.yahoo.com/",
    "https://genius.com/",
    "https://usatoday.com/",
    "https://medicalnewstoday.com/",
    "https://fedex.com/",
    "https://washingtonpost.com/",
    "https://investopedia.com/",
    "https://speedtest.net/",
    "https://spotify.com/",
    "https://cdc.gov/",
    "https://chase.com/",
    "https://hulu.com/",
    "https://msn.com/",
    "https://dictionary.com/",
    "https://weather.com/",
    "https://ups.com/",
    "https://verizon.com/",
    "https://forbes.com/",
    "https://wowhead.com/",
    "https://expedia.com/",
    "https://urbandictionary.com/",
    "https://foodnetwork.com/",
    "https://nbcnews.com/",
    
    /*"https://macys.com/",*/

    "https://ign.com/",
    "https://theguardian.com/",
    "https://cnbc.com/",
    "https://glassdoor.com/",
    "https://yellowpages.com/",
    "https://att.com/",
    "https://bbc.com/",
    "https://khanacademy.org/",
    "https://ny.gov/",
    "https://twitch.tv/",
    "https://adobe.com/",
});


using var cts = new CancellationTokenSource();

var cnt = uris.Count;

while (true) {
    var tasks = new List<Task<EZHttpResponse?>>();
    foreach (var uri in uris) {
        var req = new EZHttpRequest(
            method: "GET",
            uri: uri
        ) {
            Headers = new() {
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["User-Agent"] = "curl",
                ["Accept"] = "application/json"
            },
            VerifyTlsCertificate = false,
            FollowAutoRedirect = true,
            Verbose = true,
            //Proxy = new("socks5h://localhost:8889/")
        };
        
        async Task<EZHttpResponse?> Add()
        {
            try {
                return await EZHttp.RequestResponseAsync(
                    conductor,
                    req,
                    cts.Token
                );
            } catch (CurlException e) {
                Console.Error.WriteLine(e);
                return default;
            }
        }

        tasks.Add(
            Add()
        );
    }

    static bool NotNull<T>(T a) => null != a;

    var responses =
        Enumerable.Where(
            await Task.WhenAll(tasks),
            NotNull
        );
    foreach (var resp in responses) {
        Console.Out.WriteLine("Reading bod: {0}", resp!.Request.Uri);
        var bod = await resp.Body.ReadContentAsString();
    }
    tasks.Clear();
    await Task.Delay(500);
    Console.In.ReadLine();

}

