Notes to the assessor. 

This file will contain my musings/workings for your convenience and will act a stream of conciousnes.
I took the liberty to upgrade the project to core 3.1. having spoken with Callum, I *think* this is used in the FinTech team - so I hope this is ok

- Story 1. 
	- Will assume that JG.FinTechTest can be renamed for this solutiuon, to encapsulate the gift aid domain.
		I do hope this is ok - the readme for the test makes no mention of items which *must* remain untouched.
	- I would image in a more real world scenario, FinTech would comprise of several "domains" (e.g. PaymentProcesing, Payouts), of which GiftAid is one

	- TDD approach taken to creating the calculator
	- I've started off putting the calculator in its own project. 
		In the "real world" this might be a useful thing to do if calculations are complex enough to warrant it.. and would manifest itself as a Nuget package.
		I'm aware that for the purposes of this submission it might be overkill. I might change tack later on.
	- Looks like the tax rate is always against basic rate tax for Gift aid. 
		https://justgiving-charity-support.zendesk.com/hc/en-us/articles/204663747-Understanding-Gift-Aid-UK-Only-#:~:text=Under%20HMRC%27s%20Gift%20Aid%20scheme,that%20taxpayer%20on%20that%20donation.&text=The%20fraction%20applied%20to%20calculate,100%20which%20equals%20£25.
		However, UK basic tax rates can change and this might not always be exclusive to the UK, so this won't be hard coded
	- Initially, I assumed that Tax rates will be whole percentage numbers and never decimals. 
		However, after some research, Denmark and Egypt are examples of countries with a fracional percentage on basic rate tax. 
		No reason why this couldn't be a thing in the UK either. As such the signature is a decimal	for tax rate.
	- We will assume we want to return a value to the penny, (2 d.p)
	- We will assume we can round UP to the penny, when the whole penny is the nearest rounded value.
		We could make the calculator more dynamic in its behaviour, with round up and round down semantics, but in the interests of time for this test I won't do this.'

