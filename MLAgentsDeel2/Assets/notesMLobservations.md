**First resultfull training**



--pro's: strong cube interaction and delivery

&nbsp; con's: sometimes get "stuck" and spins in place for maybe a min or 2

&nbsp; changes: platform made bigger while spawn area of cobe is smaller os he has more naviagtion space around the cube.



**Second traing after some tests**

--pro's: quite fast delivery, delivery succes very high

&nbsp; con's: spins the whole time while moving around thus less delivery efficientie

&nbsp;	reason: probably observing to be sure to not fall of the platform



**third training debugging**

--previous issue: didnt give the cube its own X Z position nor were the rays pointing downwards slight/observable sphere too small -> so learned "i get bad reward if i move to mush so = small jittery movement and dont move from my area too mush otherwise l will fall" (end vertical offset = -0.5 )

results: a lot better!



final test: eyes back at 0° to see if it starts jittering and rotating again

--two reasons better nav: could id relative position from both floor and also the deliverbox helped it learn that. Some deliveries are succesfull but not reliable enough compared to previous solution.



what's next: let it learn finding the delivey zone instead of the route to the delivery zone

