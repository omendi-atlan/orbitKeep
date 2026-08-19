/* Orbital Command — UI interactions */

document.addEventListener("DOMContentLoaded", () => {
  // Mobile nav
  const toggle = document.getElementById("navToggle");
  const links = document.querySelector(".nav-links");
  if (toggle && links) {
    toggle.addEventListener("click", () => {
      links.classList.toggle("open");
    });
    links.querySelectorAll("a").forEach((a) => {
      a.addEventListener("click", () => links.classList.remove("open"));
    });
  }

  // Active nav on scroll
  const sections = document.querySelectorAll("section[id]");
  const navLinks = document.querySelectorAll(".nav-link");
  const onScroll = () => {
    const y = window.scrollY + 90;
    sections.forEach((sec) => {
      const top = sec.offsetTop;
      const h = sec.offsetHeight;
      if (y >= top && y < top + h) {
        navLinks.forEach((l) => l.classList.remove("active"));
        const active = document.querySelector(`.nav-link[href="#${sec.id}"]`);
        if (active) active.classList.add("active");
      }
    });
  };
  window.addEventListener("scroll", onScroll, { passive: true });

  // Code tabs
  const tabs = document.querySelectorAll(".tab");
  const panels = document.querySelectorAll(".code-panel");
  tabs.forEach((tab) => {
    tab.addEventListener("click", () => {
      const id = tab.dataset.tab;
      tabs.forEach((t) => t.classList.remove("active"));
      panels.forEach((p) => p.classList.remove("active"));
      tab.classList.add("active");
      const panel = document.getElementById(`tab-${id}`);
      if (panel) panel.classList.add("active");
    });
  });

  // Ensure video plays on mobile (muted + playsinline already set)
  const vid = document.getElementById("bgVideo");
  if (vid) {
    vid.play().catch(() => {});
  }
});
