const resetApp = () => {
  localStorage.clear();
  window.location.href = '/';
};

export { resetApp };
