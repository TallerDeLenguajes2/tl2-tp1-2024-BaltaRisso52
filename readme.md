# TL2 - Trabajo práctico 1

## Respuestas punto 2 a

**¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?**

1. *Cliente-Pedido* es una relación de composición. Un cliente es parte de un pedido y si se elimina un pedido se elimina también su cliente.
2. *Pedido-Cadete* es una relación de agregación. Un Pedido tiene un Cadete pero pueden existir Cadetes sin pedidos.
3. *Cadete-Cadetería* es una relación de agregacion. Un cadete podría existir sin pertenecer necesariamente a una cadetería específica.

**¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?**

Algunos métodos que podrían implementar estas clases:

*Cadetería*
- DarAltaPedido(Pedido): Toma un pedido nuevo y lo agrega a una lista.
- InformeDelDia(): Devuelve informacion acerca de cantidad de pedidos recibidos, entregados, de cada uno de los cadetes y un total.
- AsignarPedido(): Te permite asignar un pedido por numero a un cadete especifico por id.

*Cadete*
- JornalACobrar(): Devuelve la cantidad de dinero a cobrar segun la cantidad de pedidos entregados.
- AgregarPedido(Pedido): Agrega el pedido a la lista del cadete.
- EliminarPedido(Pedido): Elimina el pedido de la lista del cadete.

**Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, propiedades y métodos deberían ser públicos y cuáles privados**

Teniendo en cuenta los principios de abstracción y ocultamiento:

- Atributos: Deben ser privados para proteger el estado interno.
- Propiedades: Algunas serán públicas con solo un get de lectura para permitir acceso controlado a ciertos atributos.
- Métodos: Todos los métodos serán públicos para definir cómo interactuar con la clase sin exponer los detalles internos.

**¿Cómo diseñaría los constructores de cada una de las clases?**

- *Cliente*: Este constructor es completo porque recibe todos los atributos necesarios para crear un cliente con toda su información relevante
- *Pedido*: Este constructor es completo ya que inicializa todos los atributos importantes del Pedido, incluyendo la creación de una instancia de Cliente con los datos proporcionados, un número único de pedido y un estado inicial por defecto.
- *Cadete*: Este constructor es completo porque cubre todos los atributos necesarios para que el objeto Cadete esté correctamente inicializado, con una lista de pedidos vacía preparada para ser utilizada en el futuro.
- *Cadeteria*: Este constructor es completo en el sentido de que se asegura que la Cadeteria cuente con un nombre, un teléfono y una lista de cadetes desde su creación. La lista de pedidos nuevos comienza vacía, pero puede llenarse conforme la cadetería reciba pedidos.

**¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?**

No se me ocurre otra forma significativamente diferente que podría haberse realizado el diseño de estas clases.