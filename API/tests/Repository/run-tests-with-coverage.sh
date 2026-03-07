#!/bin/bash

echo "🧪 Ejecutando tests con cobertura..."
dotnet test BitTheBoss.Tests.csproj --collect:"XPlat Code Coverage"

echo ""
echo "📊 Generando reporte HTML..."
reportgenerator -reports:"TestResults/*/coverage.cobertura.xml" -targetdir:"TestResults/html" -reporttypes:Html

echo ""
echo "✅ Reporte generado en: $(pwd)/TestResults/html/index.html"
echo ""
echo "🌐 Abriendo reporte en el navegador..."
xdg-open TestResults/html/index.html 2>/dev/null || open TestResults/html/index.html 2>/dev/null || echo "Abre manualmente: $(pwd)/TestResults/html/index.html"
