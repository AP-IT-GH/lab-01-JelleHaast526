2. Beta/learning rate


# redo run1&2 incorrect learning rate
run1:
    beta: 5.0e-4 > 6.0e-4

resultaat: redelijke werking, onzekerheid/bangheid bij rond bewegen waarschijnlijk door hoge straf voor vallen. Verder lijkt hij bij de start van de training wel sneller te leren om niet te willen vallen en lijkt hij het platform beter te exploreren wat wordt veroorzaakt door  de hogere beta waaarde.

conclusie: nog geen dramatische verandering maar wel een slechtere algemene performantie op succes.

```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 111.441 s. Mean Reward: -14.656. Std of Reward: 9.723. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 205.810 s. Mean Reward: -4.048. Std of Reward: 6.461. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 300.281 s. Mean Reward: -0.162. Std of Reward: 3.992. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 393.844 s. Mean Reward: -0.071. Std of Reward: 5.809. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 495.227 s. Mean Reward: 0.957. Std of Reward: 3.475. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 591.622 s. Mean Reward: 0.636. Std of Reward: 3.947. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 684.316 s. Mean Reward: 1.179. Std of Reward: 3.507. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 781.110 s. Mean Reward: 1.560. Std of Reward: 3.405. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 876.513 s. Mean Reward: 1.382. Std of Reward: 5.022. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 974.097 s. Mean Reward: 1.883. Std of Reward: 3.192. Training.
```

run2:
    beta: 5.0e-4 > 5.0e-6 

we zien eigenlijk geen grote verandering bij veranderen van beta waarom is dit: de rewards in mijn code zijn heel direct en duidelijk hij kan eigenlijk meer één pad van rewards volgen en dat is, in de buurt van pakje en dichter verplaatsen hiervan. wanneer wordt het grbruik van beta nuttig, bijna geen observatie buiten ray receptors en deze niet weet van exacte positionering van doelen. Daarom ga ik voor de volgende drie runs de learning rate aanpassen aangezien er hier wel veranderingen te zullen zien zijn.

conclusie: doordat rewards direct en continu zijn moet hij alsware niet zoeken naar rewards. De rewards zouden schaarser moeten te vinden zijn voor nut te hebben aan beta bv enkel reward bij afleveren. Daarom zal ik overschakelen om de learning rate aan te passen.

```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 117.431 s. Mean Reward: -14.648. Std of Reward: 8.946. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 210.949 s. Mean Reward: -1.943. Std of Reward: 5.779. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 306.986 s. Mean Reward: -0.044. Std of Reward: 3.712. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 404.045 s. Mean Reward: 1.284. Std of Reward: 3.113. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 499.815 s. Mean Reward: 2.164. Std of Reward: 3.098. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 597.646 s. Mean Reward: 2.812. Std of Reward: 2.638. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 697.234 s. Mean Reward: 3.033. Std of Reward: 2.615. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 795.186 s. Mean Reward: 2.996. Std of Reward: 2.512. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 893.896 s. Mean Reward: 3.291. Std of Reward: 2.154. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 994.820 s. Mean Reward: 3.149. Std of Reward: 2.438. Training.
```

```Een kleine verduidelijking via claude:
Ik: dus laten we zeggen, ik heb een doolhof met 4 paden en deze geven alle een verschillende reward, als de beta hoog ligt dan zal deze meerdere paden proberen voor te bekijken waar hij het meeste reward uit haalt?

Claude: 14:49
Ja, precies — dat is een goed concreet voorbeeld. Maar met één nuance:

Beta zorgt niet dat de agent vooruitkijkt zoals een mens dat zou doen. Het werkt subtieler: een hoge beta straft de policy als die te zeker wordt van één keuze. De agent blijft dus langer alle paden uitproberen in plaats van na een paar goede ervaringen met pad 1 daar altijd naartoe te gaan.

Het risico zonder hoge beta in jouw doolhofvoorbeeld:

Agent vindt pad 2 toevallig eerst en krijgt reward
Policy convergeert snel naar "altijd pad 2"
Pad 4 met de hoogste reward wordt nooit ontdekt
Met hogere beta:

Agent blijft ook pad 3 en 4 uitproberen ondanks dat pad 2 al werkt
Grotere kans dat hij pad 4 ontdekt en daar naartoe convergeert
Dus jouw intuïtie klopt — het is alleen geen vooruitkijken maar eerder gedwongen blijven uitproberen. Dat is precies de waarde van beta in omgevingen waar de beste oplossing niet de eerste gevonden oplossing is.
```


run3:
    learning rate: 3.0 e-4 > 1.0e-5

Ai blijft plaatselijke ronddraaien, dit komt mede doordat de agent zo traag leert dat hij ondekt dat ronddraaien de penalty niet te erg maakt, hij kan niet binnen de episode leren om bij het pakket in de buurt te komen en zeker niet het afleveren hier van.

```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 132.673 s. Mean Reward: -24.877. Std of Reward: 15.442. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 236.093 s. Mean Reward: -45.813. Std of Reward: 23.358. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 332.531 s. Mean Reward: -30.139. Std of Reward: 27.234. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 432.592 s. Mean Reward: -37.152. Std of Reward: 16.378. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 529.019 s. Mean Reward: -37.432. Std of Reward: 18.180. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 624.631 s. Mean Reward: -45.978. Std of Reward: 25.640. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 718.820 s. Mean Reward: -40.386. Std of Reward: 26.369. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 813.743 s. Mean Reward: -49.730. Std of Reward: 47.144. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 909.078 s. Mean Reward: -43.279. Std of Reward: 41.874. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 1003.885 s. Mean Reward: -44.498. Std of Reward: 39.638. Training.

```

run4:
    learning rate 3.0 e-4 > 1.0 e-2

We zien een mean reward waarbij de agent aan het leren is maar dat de std heel gevarieerd is wat het aantonen geeft van een choatische policy.
```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 115.777 s. Mean Reward: -28.303. Std of Reward: 22.760. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 218.653 s. Mean Reward: -40.244. Std of Reward: 26.991. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 324.038 s. Mean Reward: -84.057. Std of Reward: 0.000. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 431.506 s. No episode was completed since last summary. Training.

**komt vast te zitten, geen nut in verdere training.
```

overschoot, kleine beweging plots reward > direct zo beginnen blijven bewegen omdat hier reward van heeft gehad. Updates zijn de chaotisch voor goed te trainen. Stuitert chaotisch heen en weer bij de policy update.

run5: 
    learning rate 3.0 e-4 > 5.0e-3

```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 131.238 s. Mean Reward: -17.817. Std of Reward: 15.664. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 233.407 s. Mean Reward: -32.165. Std of Reward: 25.436. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 336.718 s. Mean Reward: -20.903. Std of Reward: 18.293. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 444.385 s. Mean Reward: -10.481. Std of Reward: 12.159. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 547.875 s. Mean Reward: -13.771. Std of Reward: 12.731. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 658.249 s. Mean Reward: -5.825. Std of Reward: 8.467. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 763.076 s. Mean Reward: -0.129. Std of Reward: 3.950. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 870.538 s. Mean Reward: 0.419. Std of Reward: 4.006. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 978.774 s. Mean Reward: 0.303. Std of Reward: 4.544. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 1086.090 s. Mean Reward: 1.099. Std of Reward: 3.290. Training.
```



3. Beloningssysteem, bijv. de grootte van de afstraffing van de Agent om niets te doen

run6:
    fallPunishment= -1 > -5

```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 116.348 s. Mean Reward: -33.294. Std of Reward: 22.026. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 207.681 s. Mean Reward: -8.977. Std of Reward: 13.370. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 305.383 s. Mean Reward: -2.480. Std of Reward: 4.789. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 403.352 s. Mean Reward: -0.295. Std of Reward: 5.170. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 502.267 s. Mean Reward: 1.317. Std of Reward: 3.375. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 602.271 s. Mean Reward: 2.787. Std of Reward: 1.826. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 701.134 s. Mean Reward: 2.876. Std of Reward: 2.784. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 798.770 s. Mean Reward: 3.494. Std of Reward: 1.439. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 903.107 s. Mean Reward: 3.700. Std of Reward: 1.606. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 1007.012 s. Mean Reward: 3.897. Std of Reward: 1.288. Training.
```

run7: 
    time penalty= -0.005 > -0.01

```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 129.756 s. Mean Reward: -23.369. Std of Reward: 20.621. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 223.609 s. Mean Reward: -10.713. Std of Reward: 6.552. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 317.810 s. Mean Reward: -5.669. Std of Reward: 2.859. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 417.336 s. Mean Reward: -4.596. Std of Reward: 3.121. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 517.221 s. Mean Reward: -3.405. Std of Reward: 3.984. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 611.488 s. Mean Reward: -3.113. Std of Reward: 3.979. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 709.666 s. Mean Reward: -2.529. Std of Reward: 4.294. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 808.116 s. Mean Reward: -1.483. Std of Reward: 4.150. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 906.645 s. Mean Reward: -1.505. Std of Reward: 4.370. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 1008.404 s. Mean Reward: -0.860. Std of Reward: 4.298. Training.
```

run8:
    time penalty= -0.005 > -1

```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 128.004 s. Mean Reward: -1505.969. Std of Reward: 2007.958. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 229.999 s. Mean Reward: -157.745. Std of Reward: 104.705. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 335.398 s. Mean Reward: -113.911. Std of Reward: 61.944. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 438.205 s. Mean Reward: -99.218. Std of Reward: 40.391. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 541.835 s. Mean Reward: -90.890. Std of Reward: 30.362. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 646.933 s. Mean Reward: -92.495. Std of Reward: 32.712. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 751.815 s. Mean Reward: -93.603. Std of Reward: 34.530. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 853.873 s. Mean Reward: -92.201. Std of Reward: 35.548. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 953.646 s. Mean Reward: -188.878. Std of Reward: 2496.936. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 1058.671 s. Mean Reward: -129.836. Std of Reward: 675.727. Training.
```

run9:

package fall= -1f > -5f
```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 130.948 s. Mean Reward: -21.542. Std of Reward: 20.426. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 219.624 s. Mean Reward: -11.461. Std of Reward: 15.856. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 320.042 s. Mean Reward: -2.449. Std of Reward: 5.615. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 419.027 s. Mean Reward: -0.179. Std of Reward: 5.047. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 511.116 s. Mean Reward: 1.243. Std of Reward: 3.471. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 605.751 s. Mean Reward: 1.867. Std of Reward: 3.269. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 704.497 s. Mean Reward: 1.988. Std of Reward: 3.088. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 801.454 s. Mean Reward: 2.932. Std of Reward: 2.274. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 899.160 s. Mean Reward: 3.084. Std of Reward: 2.189. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 998.122 s. Mean Reward: 3.083. Std of Reward: 2.253. Training.
```

run10:
    distanceimprovement mult = 0.5f > 5f

```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 111.271 s. Mean Reward: -16.779. Std of Reward: 29.072. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 206.143 s. Mean Reward: 8.027. Std of Reward: 7.077. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 301.611 s. Mean Reward: 10.985. Std of Reward: 6.182. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 400.317 s. Mean Reward: 12.084. Std of Reward: 5.750. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 494.485 s. Mean Reward: 13.182. Std of Reward: 5.802. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 589.492 s. Mean Reward: 12.626. Std of Reward: 6.772. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 691.132 s. Mean Reward: 13.363. Std of Reward: 5.346. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 790.837 s. Mean Reward: 13.080. Std of Reward: 5.631. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 888.512 s. Mean Reward: 13.372. Std of Reward: 5.627. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 990.682 s. Mean Reward: 13.192. Std of Reward: 5.588. Training.
```

run11:
    ogen recht i.p.v. dat deze de grond zien.

Ik wilde zeker deze nog eens testen of deze nu effevtief het probleem van het onneindig ronddraaien veroorzaakt, na hiermee te trainen zonder dat de ogen naar de grond gericht zijn bekomen we het volgende resultaat waarbij de rond dwaald op een bepaald punt omdat hij niet weet waarom hij een negatieve reward krijgt.
```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 110.895 s. Mean Reward: -13.414. Std of Reward: 11.815. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 201.596 s. Mean Reward: -18.990. Std of Reward: 16.403. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 295.518 s. Mean Reward: -12.862. Std of Reward: 12.853. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 386.071 s. Mean Reward: -8.676. Std of Reward: 12.487. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 477.975 s. Mean Reward: -6.783. Std of Reward: 8.592. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 567.230 s. No episode was completed since last summary. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 657.026 s. No episode was completed since last summary. Training.
```

run12:
zelfde als de origineele versie alleen met 100k ipv 200k stappen voor een betere refferentie 
```
[INFO] CubeAgentRays. Step: 10000. Time Elapsed: 143.806 s. Mean Reward: -11.825. Std of Reward: 10.815. Training.
[INFO] CubeAgentRays. Step: 20000. Time Elapsed: 301.158 s. Mean Reward: -16.232. Std of Reward: 15.442. Training.
[INFO] CubeAgentRays. Step: 30000. Time Elapsed: 462.274 s. Mean Reward: -16.700. Std of Reward: 16.589. Training.
[INFO] CubeAgentRays. Step: 40000. Time Elapsed: 626.822 s. Mean Reward: -9.179. Std of Reward: 6.320. Training.
[INFO] CubeAgentRays. Step: 50000. Time Elapsed: 736.387 s. Mean Reward: -6.894. Std of Reward: 7.201. Training.
[INFO] CubeAgentRays. Step: 60000. Time Elapsed: 842.713 s. Mean Reward: -6.310. Std of Reward: 6.714. Training.
[INFO] CubeAgentRays. Step: 70000. Time Elapsed: 1006.729 s. Mean Reward: -5.364. Std of Reward: 8.726. Training.
[INFO] CubeAgentRays. Step: 80000. Time Elapsed: 1126.394 s. Mean Reward: -2.213. Std of Reward: 7.426. Training.
[INFO] CubeAgentRays. Step: 90000. Time Elapsed: 1222.722 s. Mean Reward: 0.702. Std of Reward: 6.025. Training.
[INFO] CubeAgentRays. Step: 100000. Time Elapsed: 1317.622 s. Mean Reward: 2.099. Std of Reward: 4.338. Training.
```