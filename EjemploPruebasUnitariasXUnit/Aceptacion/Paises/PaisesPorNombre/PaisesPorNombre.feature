Característica: PaisesPorNombre
	Para obtener información de
	paises cuyo nombre a partir de su
	nombre

@mytag
Escenario: Búsqueda por nombre exacto con resultado
	Dado que la API de terceros devolvera el codigo 200 y el json '[{"name":"Argentina","callingCodes":["54"],"alpha2Code":"AR","alpha3Code":"ARG","region":"Americas","subregion":"South America","population":43590400,"borders":["BOL","BRA","CHL","PRY","URY"]}]'
	Cuando se invoca el endpoint '/pais/nombre' con los parametros 'argentina'
	Entonces la API devuelve el codigo 200 y el json '[{"name":"Argentina","callingCodes":["54"],"alpha2Code":"AR","alpha3Code":"ARG","region":"Americas","subregion":"South America","population":43590400,"borders":["BOL","BRA","CHL","PRY","URY"]}]'


Escenario: La API de terceros NO devuelve 200 o 204
	Dado que la API de terceros devolvera el codigo 500
	Cuando se invoca el endpoint '/pais/nombre' con los parametros 'argentina'
	Entonces la API devuelve codigo 500
