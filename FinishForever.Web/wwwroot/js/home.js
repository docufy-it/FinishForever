window.initHomepageAnimations = () =>
{
    const cards =
        document.querySelectorAll(".story-card");

    if (!cards.length)
    {
        console.log("No story cards found");
        return;
    }

    console.log(`Found ${cards.length} story cards`);

    const observer =
        new IntersectionObserver(
            (entries) =>
            {
                entries.forEach(entry =>
                {
                    if (!entry.isIntersecting)
                    {
                        return;
                    }

                    const card =
                        entry.target;

                    card.classList.add("visible");

                    const cells =
                        card.querySelectorAll(".heatmap-cell");

                    cells.forEach((cell, index) =>
                    {
                        cell.style.transitionDelay =
                            `${index * 4}ms`;
                    });

                    observer.unobserve(card);
                });
            },
            {
                threshold: 0.15,
                rootMargin: "0px 0px -10% 0px"
            });

    cards.forEach(card =>
    {
        observer.observe(card);
    });
};

document.addEventListener(
    "DOMContentLoaded",
    () =>
    {
        console.log("home.js loaded");
    });