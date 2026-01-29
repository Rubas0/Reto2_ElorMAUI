# ElorMAUI

ElorMAUI es la aplicación MAUI del Framework Educativo Elor. Su objetivo principal es listar centros educativos, mostrar detalles de cada centro y ofrecer información geográfica y meteorológica.

Características principales
- Listado paginado y filtrable de centros (por tipo, territorio y municipio).
- Página de detalle de cada centro con:
  - Información del centro (dirección, municipio, teléfono, etc.).
  - Clima: temperatura actual y previsión para los próximos días (obtenido desde WeatherAPI.com).
  - Mapa interactivo con marcador en la ubicación del centro (Mapbox).
- Soporte para cambiar tipo de mapa (calles, outdoor, satélite).
- Mensajes de estado y manejo básico de errores en la UI (carga, fallo de clima, mapa sin coordenadas).
- Código orientado a ser simple y entendible para estudiantes (fácil de mantener y ampliar).

Requisitos
- .NET MAUI (versión compatible con el proyecto).
- Acceso a Internet para llamadas a las APIs externas.
- Clave válida de WeatherAPI.com (si se usa WeatherAPI).
- Token de Mapbox para el mapa.

Ejecución
1. dotnet restore
2. dotnet build
3. dotnet run

Puntos de evaluación (rúbrica)
- Temperatura actual y previsión: implementado en la vista de detalle.
- Mapa con marcador: implementado con Mapbox.
- Interfaz responsive y limpieza de UI: diseñada para Bootstrap/estilos compatibles con MAUI Blazor.

FALTA
- Checkear boostrap.
- Documentar códigoa.
