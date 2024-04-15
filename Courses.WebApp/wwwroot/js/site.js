// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    const toggleBookmark = (e, courseId, isBookmarked) => {
    e.preventDefault()
    const form = new FormData()
    form.append('courseId', courseId)
    form.append('isBookmarked', !isBookmarked)

    fetch('Courses/bookmark',{
    method: "POST",
    body: form
}).then(res => res.text()).then(html => {

        const element = document.getElementById(`bookmark-${courseId}`)
        const parser = new DOMParser();
        const parsedResponse = parser.parseFromString(html, 'text/html');

        const newElement = parsedResponse.body.firstChild;
       element.replaceWith(newElement) 
}).catch(e => {})
}