document.addEventListener('input', e => {
    const target = e.target.dataset.strengthTarget;
    if (!target) return;

    const v = e.target.value || '';
    let score = 0;
    if (v.length >= 8) score++;
    if (/[A-Z]/.test(v)) score++;
    if (/[a-z]/.test(v)) score++;
    if (/\d/.test(v)) score++;
    if (/[@#$%*&!]/.test(v)) score++;
    if (v.length >= 12) score++;

    const labels = ['Too short', 'Weak', 'Fair', 'Good', 'Strong', 'Very strong'];
    const pcts = [10, 25, 45, 65, 85, 100];
    const colors = ['#dc2626', '#dc2626', '#f59e0b', '#3b82f6', '#10b981', '#059669'];
    const i = Math.min(score, 5);

    const box = document.querySelector(target);
    if (!box) return;
    box.innerHTML = `<div class="pw-bar"><div class="pw-fill" style="width:${pcts[i]}%;background:${colors[i]}"></div></div>
                     <span class="pw-label" style="color:${colors[i]}">${labels[i]}</span>`;
});