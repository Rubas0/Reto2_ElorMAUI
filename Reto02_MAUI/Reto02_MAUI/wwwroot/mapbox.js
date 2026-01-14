// Función para inicializar el mapa de Mapbox
window.initializeMap = (token, latitude, longitude, zoom, centerName, centerAddress) => {
    // Configurar token de acceso
    mapboxgl.accessToken = token;

    // Crear el mapa
    const map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/streets-v12',
        center: [longitude, latitude], // Mapbox usa [longitud, latitud]
        zoom: zoom
    });

    // Añadir controles de navegación (zoom, rotación)
    map.addControl(new mapboxgl.NavigationControl());

    // Crear popup con información del centro
    const popup = new mapboxgl.Popup({
        offset: 25,
        closeButton: true,
        closeOnClick: false
    })
        .setHTML(`<div style="padding: 10px;">
                <h3 style="margin: 0 0 10px 0; color: #0066CC;">${centerName}</h3>
                <p style="margin: 5px 0;">${centerAddress}</p>
              </div>`);

    // Crear marcador personalizado (color naranja corporativo)
    const marker = new mapboxgl.Marker({ color: '#FF6B35' })
        .setLngLat([longitude, latitude])
        .setPopup(popup)
        .addTo(map);

    // Mostrar popup automáticamente después de cargar
    setTimeout(() => {
        marker.togglePopup();
    }, 500);
};