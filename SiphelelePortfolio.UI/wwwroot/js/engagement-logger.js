function logEngagement(eventType, projectId, projectTitle) {
    fetch('/api/project/log/' + eventType, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ projectId: projectId, projectTitle: projectTitle })
    }).catch(() => {});
}