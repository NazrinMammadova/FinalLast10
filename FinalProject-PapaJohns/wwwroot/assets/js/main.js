const themeToggle = document.getElementById('theme-toggle');
const themeIcon = document.getElementById('theme-icon');

function toggleDarkTheme() {
  document.body.classList.toggle('dark-theme');
  updateTheme();
}

function updateTheme() {
  const isDarkTheme = document.body.classList.contains('dark-theme');
  updateThemeIcon(isDarkTheme);
  saveThemePreference(isDarkTheme);
}

function updateThemeIcon(isDarkTheme) {
  themeIcon.classList.remove('fa-moon', 'fa-sun');
  themeIcon.classList.add(isDarkTheme ? 'fa-sun' : 'fa-moon');
}

function saveThemePreference(isDarkTheme) {
  const currentTheme = isDarkTheme ? 'dark' : 'light';
  localStorage.setItem('theme', currentTheme);
}

themeToggle.addEventListener('click', () => {
  toggleDarkTheme();
});

document.addEventListener('DOMContentLoaded', function () {
  const userThemePreference = localStorage.getItem('theme');
  const isDarkTheme = userThemePreference === 'dark';

  if (isDarkTheme) {
    document.body.classList.add('dark-theme');
  } else {
    document.body.classList.remove('dark-theme');
  }

  updateThemeIcon(isDarkTheme);
});

themeToggle.addEventListener('click', () => {
  const isMoon = themeIcon.classList.contains('fa-moon');
  const isDarkTheme = isMoon ? false : true;

  updateThemeIcon(isDarkTheme);
  saveThemePreference(isDarkTheme);
});




$(document).ready(function () {
    $(document).on("keyup", ".input-search", function () {
        $(".header-search li").slice(0).remove();
        let value = $(this).val();
        $.ajax({
            method: "get",
            url: `/product/searchproduct?text=${value}`,
            success: function (data) {
                $(".header-search").append(data);
            }

        })
    });   



});




document.getElementById('goUp').addEventListener('click', function () {
    window.scrollTo({
        top: 0,
        behavior: 'smooth' // Animasyonlu bir ?ekilde sayfan?n en üstüne gitmek için
    });
});
