const patternButtons = document.querySelectorAll('#patternList button[data-section]');
patternButtons.forEach(btn => {
  btn.addEventListener('click', () => {
    document.querySelectorAll('.pattern-section').forEach(s => s.classList.add('d-none'));
    patternButtons.forEach(b => b.classList.remove('active'));
    btn.classList.add('active');
    document.getElementById(btn.dataset.section).classList.remove('d-none');
  });
});

async function callEndpoint(path, params = {}) {
  const base = document.getElementById('baseUrl').value.replace(/\/$/, '');
  const url = new URL(base + '/' + path, window.location.origin);
  Object.keys(params).forEach(k => {
    if (params[k] !== undefined && params[k] !== null && params[k] !== '') {
      url.searchParams.append(k, params[k]);
    }
  });

  const respEl = document.getElementById('response');
  respEl.textContent = `Calling ${url.toString()}...`;

  try {
    const res = await fetch(url.toString(), { method: 'GET' });
    const text = await res.text();
    respEl.textContent = `HTTP ${res.status} ${res.statusText}\n\n${text}`;
  } catch (err) {
    respEl.textContent = `Request failed: ${err}`;
  }
}