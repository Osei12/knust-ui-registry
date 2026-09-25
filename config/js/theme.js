
export function setTheme(theme) {
    if (theme === 'dark') {
        document.documentElement.classList.add('dark');
    } else {
        document.documentElement.classList.remove('dark');
    }
}

export function getPreferredTheme() {
    return localStorage.getItem('theme') || (window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
}

export function saveTheme(theme) {
    localStorage.setItem('theme', theme);
}

export function initScrollSpy() {
    const sections = document.querySelectorAll('section[id]');
    const navLinks = document.querySelectorAll('.nav-link-section');

    const options = {
        root: null,
        rootMargin: '-20% 0px -70% 0px',
        threshold: 0
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const id = entry.target.getAttribute('id');
                updateActiveLink(id);
            }
        });
    }, options);

    sections.forEach(section => {
        observer.observe(section);
    });

    function updateActiveLink(id) {
        navLinks.forEach(link => {
            link.classList.remove('active-spy');
            if (link.getAttribute('data-section') === id) {
                link.classList.add('active-spy');
            }
        });
    }

    // Handle clicks on navigation links for smooth scrolling
    navLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            const href = link.getAttribute('href');
            if (href && href.includes('#')) {
                const id = href.split('#')[1];
                const element = document.getElementById(id);
                if (element) {
                    e.preventDefault();
                    element.scrollIntoView({
                        behavior: 'smooth'
                    });
                    // Update URL without jump
                    window.history.pushState(null, '', href);
                    updateActiveLink(id);
                }
            }
        });
    });

    // Handle initial fragment in URL
    const handleInitialHash = () => {
        if (window.location.hash) {
            const id = window.location.hash.substring(1);
            const element = document.getElementById(id);
            if (element) {
                // Wait for any layouts to settle
                setTimeout(() => {
                    element.scrollIntoView({ behavior: 'smooth' });
                }, 300);
            }
        }
    };

    if (document.readyState === 'complete') {
        handleInitialHash();
    } else {
        window.addEventListener('load', handleInitialHash);
    }
}

export function registerClickOutside(dotnetHelper, elementId, methodName) {
    const handler = (e) => {
        const element = document.getElementById(elementId);
        if (element && !element.contains(e.target)) {
            dotnetHelper.invokeMethodAsync(methodName);
        }
    };
    window.addEventListener('mousedown', handler);
    return {
        dispose: () => window.removeEventListener('mousedown', handler)
    };
}

export async function copyToClipboard(text) {
    try {
        await navigator.clipboard.writeText(text);
        return true;
    } catch (err) {
        console.error('Failed to copy: ', err);
        return false;
    }
}

export function scrollCarousel(carouselId, direction) {
    const carousel = document.getElementById(carouselId);
    if (!carousel) return;
    
    const content = carousel.querySelector('.overflow-x-auto');
    if (!content) return;
    
    const scrollAmount = content.clientWidth;
    const maxScrollLeft = content.scrollWidth - content.clientWidth;
    const currentScrollLeft = content.scrollLeft;

    console.log(`Scrolling carousel ${carouselId} in direction ${direction} by ${scrollAmount}px`);
    console.log(`MaxScrollLeft ${maxScrollLeft} |||||||||||||| ${direction} CurrentScrollLeft ${currentScrollLeft}`);

    // GO TO NEXT
    if(direction === 'next'){

    }

    content.scrollBy({
        left: direction === 'next' ? scrollAmount : -scrollAmount,
        behavior: 'smooth'
    });
}
