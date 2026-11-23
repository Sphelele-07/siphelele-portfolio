(function () {
    const modal = document.getElementById('confirmModal');
    if (!modal) return;

    const titleEl = document.getElementById('confirmTitle');
    const msgEl = document.getElementById('confirmMessage');
    const okBtn = document.getElementById('confirmOkBtn');
    const cancelBtn = document.getElementById('confirmCancelBtn');

    let pendingForm = null;

    function hide() { modal.classList.add('hidden'); pendingForm = null; }

    okBtn.addEventListener('click', () => {
        if (pendingForm) {
            const f = pendingForm;
            pendingForm = null;
            modal.classList.add('hidden');
            f.submit();
        }
    });
    cancelBtn.addEventListener('click', hide);
    modal.addEventListener('click', e => { if (e.target === modal) hide(); });

    document.addEventListener('submit', e => {
        const form = e.target;
        if (!form.matches('form[data-confirm]') || form.dataset.confirmed === 'true') return;
        e.preventDefault();
        pendingForm = form;
        titleEl.textContent = 'Are you sure?';
        msgEl.textContent = form.dataset.confirm || 'This action cannot be undone.';
        modal.classList.remove('hidden');
    }, true);
})();