############  Guia de ejecución funcional Proyecto BlazorCrudAssessment, registro y administración de Productos ############

1. Clonar repositorio:
git clone https://github.com/animationware/BlazorCrudAssessment.git
2. Acceder al directorio descargado usando el siguiente comando:
cd BlazorCrudAssessment
2. Una vez clonado dicho repositorio todos los archivos y componentes requeridos para su ejecución estaran completamente listos para inicializar el Proyecto.
3. Es de vital importancia ejecutar inicialmente en una terminal aislada el servidor asociado a la API Rest ubicado en el directorio OrderRegistry.API haciendo uso de los siguiente comando:
cd OrderRegistry.API
dotnet run
4. Posteriormente sera posible inicializar en otra terminal aislada la UI asociada al Proyecto accediendo al repositorio OrderRegistry.UI haciendo uso de los siguiente comandos:
cd OrderRegistry.UI
dotnet run
5. Una vez inicializadas las respectivas interfaces, tanto de API REST como a nivel FrontEnd UI, será posible efectuar las validaciones funcionales del Proyecto a traves de la siguiente interfaz Web, especificando el puerto correspondiente de ser necesario:
http://localhost:5248/
6. En la interfaz Web podrán realizarse las diferentes pruebas correspondientes a nivel de creación, actualización, edición y borrado de items existentes en la base de datos desde una interfaz completamente intuitiva, a traves de la API Rest OrderRegistry.API.

Notas del Proyecto: 
1. Dentro del proyecto descargado ya se encuentra creada una base de datos SQLite en el directorio OrderRegistry.API en la cual se gestionan todos los cambios a traves de los diferentes componentes.
2. Este proyecto pretende seguir las mejores practicas a nivel de Desarrollo de Software haciendo uso de los principios fundamentales de Clean Architecture.

*### Autor: Michael Romero Ortega ####*
