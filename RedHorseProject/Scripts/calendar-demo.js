/*
 Template Name: Xacton - Admin & Dashboard Template
 Author: Myra Studio
 File: Calendar
*/


!function($) {
  "use strict";

  var CalendarPage = function() {};

  CalendarPage.prototype.init = function() {

      //checking if plugin is available
      if ($.isFunction($.fn.fullCalendar)) {
          /* initialize the external events */
          $('#external-events .fc-event').each(function() {
              // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
              // it doesn't need to have a start or end
              var eventObject = {
                  title: $.trim($(this).text()) // use the element's text as the event title
              };

              // store the Event Object in the DOM element so we can get to it later
              $(this).data('eventObject', eventObject);

              // make the event draggable using jQuery UI
              $(this).draggable({
                  zIndex: 999,
                  revert: true, // will cause the event to go back to its
                  revertDuration: 0 //  original position after the drag
              });
          });
          
          /* initialize the calendar */

          var date = new Date();
          var d = date.getDate();
          var m = date.getMonth();
          var y = date.getFullYear();

          $('#calendar').fullCalendar({
              header: {
                  left: 'prev,next today',
                  center: 'title',
                  right: 'month,basicWeek,basicDay'
              },
              editable: true,
              eventLimit: true, // allow "more" link when too many events
              droppable: true, // this allows things to be dropped onto the calendar !!!
              drop: function(date, allDay) { // this function is called when something is dropped

                  // retrieve the dropped element's stored Event Object
                  var originalEventObject = $(this).data('eventObject');

                  // we need to copy it, so that multiple events don't have a reference to the same object
                  var copiedEventObject = $.extend({}, originalEventObject);

                  // assign it the date that was reported
                  copiedEventObject.start = date;
                  copiedEventObject.allDay = allDay;

                  // render the event on the calendar
                  // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                  $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

                  // is the "remove after drop" checkbox checked?
                  if ($('#drop-remove').is(':checked')) {
                      // if so, remove the element from the "Draggable Events" list
                      $(this).remove();
                  }

              },



              events: [{
                  
                  }],
                  dayClick: function (date, jsEvent, view) {
                  // T�klanan g�n� al�n
                  var selectedDate = date.format('YYYY-MM-DD');

                  // Saatleri listeleyin (�rnek olarak belirli saatleri kullan�yoruz)
                  var hours = [
                      '09:00', '10:00', '11:00', '12:00', '13:00',
                      '14:00', '15:00', '16:00', '17:00', '18:00'
                  ];

                  // Saatleri modal i�ine ekleyin
                  var timeListHtml = '<ul>';
                  hours.forEach(function (hour) {
                      timeListHtml += `<li><button class="btn btn-primary btn-sm mb-1">${hour}</button></li>`;
                  });
                  timeListHtml += '</ul>';

                  // Modal i�indeki listeyi g�ncelleyin
                  $('#timeList').html(timeListHtml);

                  // Modal ba�l���n� se�ilen tarihe g�re g�ncelleyin
                  $('#timeModalLabel').text(' ' + selectedDate);

                  // Modali g�ster
                  $('#timeModal').modal('show');
              },
          });
          
           /*Add new event*/
          // Form to add new event

          

      }
      else {
          alert("Calendar plugin is not installed");
      }
  },
  //init
  $.CalendarPage = new CalendarPage, $.CalendarPage.Constructor = CalendarPage
}(window.jQuery),

//initializing 
function($) {
  "use strict";
  $.CalendarPage.init()
}(window.jQuery);