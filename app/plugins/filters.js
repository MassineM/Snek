import Vue from "vue";
import moment from "moment";

Vue.filter("capitalize", (val) => val.toUpperCase());

Vue.filter("date", (val) => moment(val).format("YYYY/MM/DD"));
Vue.filter("datetime", (val) => moment(val).format("YYYY/MM/DD - h:mm a"));

// Specific attribute of a date
Vue.filter("MONTH", (val) => moment(val).format("MMMM").toUpperCase());
Vue.filter("day", (val) => moment(val).format("Do"));
Vue.filter("year", (val) => moment(val).format("YYYY"));

Vue.filter("field_color", (val) => {
  switch (val) {
    case "onboarding":
      return "#8a7b60";

    case "key-account-management":
      return "#88ba14";

    case "management-and-leadership":
      return "#f08a00";

    case "technical":
      return "#791d41";

    default:
      // Finance
      return "#135975";
  }
});

Vue.filter("days", (val) => {
  var a = moment(val[0]);
  var b = moment(val[1]);
  if (a > b) return a.diff(b, "days");
  return b.diff(a, "days");
});
