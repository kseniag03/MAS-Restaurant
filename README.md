# MAS-Restaurant
## ИДЗ №3 КПО

Консольное приложение « Мультиагентная система управления рестораном »

Список нюансов для уточнения <br>
// wanted

Объяснение выбора паттернов <br>
// wanted


<br><br>


// НИЖЕ ШПОРА по https://docs.google.com/document/d/1iN_ofZj_wyaFpWY3ME1Ocp1_2gpZ9o7PdUJt_1Y_3Wg/edit

input: файлы JSON <br>
output: файлы JSON с логами обслуживания посетителей и работы агентов (запуск, действие, результат, уничтожение)

Требования
1. корректная ООП-реализация программы: 4 балла
2. доп. функционал параллелизм в процессах: +2
3. доп. функционал оценка времени ожидания заказов (в момент старта): +1
3. доп. функционал оценка времени ожидания заказов (в любое время на любом этапе выполнения): +1
4. доп. функционал проверка и обработка ошибок входных данных: +1
5. корректное и обоснованное применение паттерн(а/ов) проектирования МАС: +1

Штрафы
1. в папке проекта нет jar-файла: -1
2. входные данные не в JSON: -1
3. выходные данныене в JSON: -1

Бонусы
1. МАС на платформе Jade (без онтологий): +1
2. МАС на платформе Jade (с онтологией): +2

https://jade.tilab.com


<br><br>


Общие требования к системе
1. компоненты системы — агенты
2. добавление компонента не приводит к переписыванию всей системы
3. все заказы выполняются оперативно
4. посетителю сообщено ~время ожидания заказа
 
Более детальные требования
1. агенты взаимодействуют через вызовы методов путем отправки сообщений
2. агенты поддерживают методы обнаружения других агентов определенного типа
3. специфичные агенты автоматически выбирают нужных агентов из списка обнаруженных
4. (optional) супервизор замечает задержку выполнения агентов и ставит на задачу нового агента
5. агенты контролируют процесс обслуживания с момента оформления до выдачи заказа
6. агенты отвечают на запросы по времени задачи (выполнение заказа, процесс готовки конкретики, операции в рамках процесса)

Любой агент должен иметь методы
1. отправить сообщение другому агенту
2. прочитать входящее сообщение от другого агента
3. самоуничтожиться (после выполнения задачи)
4. приостановить выполнение (для возобновления нужно сообщение от агента выше в иерархии). 
5. агенты создаются управляющим агентом


<br><br>

 
Обязательные агенты
1. посетитель
2. заказ
3. супервизор (управляющий агент)
4. склад
5. продукт
6. процесс
7. операциия
8. повар
9. оборудование

Агент посетителя // паттерн посетитель ?
1. добавить элемент меню в заказ
2. удалить элемент меню из заказа
3. отключить пункт меню из-за недоступности необходимого ресурса
4. включить ранее отключенный пункт меню из-за появления необходимого ресурса
5. попросить супервизора создать экземпляр заказа
6. отменить заказ (уничтожается соответствующий заказ)
7. получить обновленную информацию о ~времени ожидания заказа от заказа
 
Агент заказа // паттерн цепочка обязанностей ?
1. хранит список заказанных элементов меню
2. принимает и обрабатывает сообщения от супервизора
3. отправляет посетителю инфу о времени ожидания заказа
4. получает от процессов оценку времени ожидания
5. обрабатывает ответ о времени ожидания готовности
6. резервирует необходимые ресурсы для выполнения заказа
7. отменяет резервирование ресурса при отмене заказа
8. обрабатывает ответ от продуктов о результате резервирования
10. (optional) заказ может не быть обработанным после первого завершения такого поведения из-за инфы об очереди от супервизора
 
Агент элемента меню
1. cодержит списки созданных процессов, операций и продуктов для создания конкретного элемента меню из заказа
2. уничтожается, когда заказ выполнен
 
Супервизор // паттерн абстрактная фабрика ?
1. управляет другими агентами для выполнения заказов
2. запускает процесс создания нового заказа
3. по запросу от посетителя создает заказ
4. после выполнения заказа контролирует его уничтожение
5. вызывает метод резервированию у склада для каждого экземпляра заказанного элемента меню из заказа с заданным объемом конкретного продукта
6. создает и уничтожает прочих агентов
 
Агент склада // паттерн команда + снимок ?
1. содержит список активных продуктов
2. содержит таблицу имеющихся на складе продуктов с объемами остатков
3. проверяет наличие на складе достаточного объема продукта для резервирования под экземпляр элемента меню из заказа
4. создает продукт при успешной проверке
 
Агент продукта // паттерн состояние ?
1. представляет объем продукта для приготовления элемента меню
2. создается складом при формировании заказа
3. при создании продукта его необходимый объем резервируется (если в наличии)
4. при отмене заказа отменить резервирование
5. после создания экземпляра элемента меню уничтожает продукт, переписывая объем

Агент процесса
1. контролирует выполнение процесса из технологических операций
2. возвращает инфу о ~времени ожидания выполнения процесса
3. обращение к поварам или оборудованию
 
Агент операции
1. запрашивает супервизора зарезервировать повара и оборудования для выполнения операции процесса
 
Агент повара
1. представляет повара ресторана, взаимодействует с ним через кухонный терминал
2. назначает выполнение операции
3. (optional) отменяет / приостанавливает назначенную операцию
4. (optional) возобновляет приостановленную им операцию
5. принимает события, связанные с выполнением операций («приступил», «выполнил», «отменил»)
 
Агент оборудования
1. конкретная единица кухни (печь, микроволновка, ...)
2. супервизор управляет его использованием через повара, использующего оборудование в рамках выполнения назначенной ему определенной операции процесса приготовления элемента меню


<br><br>


Задачи к реализации
1. имитация работы ресторана за одну смену в модельном времени с ускорением в n раз относительно реального
2. (optional) возможностью задать скорость имитации (от реального времени до ускорения в 100 раз)
3. (optional) смоделировать поток посетителей, включая переполнение ресторана в обеденное & вечернее время и перекати-поле в утреннее
4. (optional) удаление из меню блюд, содержащих продукты вне вклада
 
 


